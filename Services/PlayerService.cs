using Baza.Data;
using Baza.Models.Entities;
using Baza.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Baza.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(ApplicationDbContext context, ILogger<PlayerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<PlayerResponseDto>> GetAllPlayersAsync()
        {
            return await _context.Players
                .OrderByDescending(p => p.Score)
                .Select(p => new PlayerResponseDto
                {
                    Id = p.Id,
                    Nickname = p.Nickname,
                    Score = p.Score
                })
                .ToListAsync();
        }

        public async Task<PlayerResponseDto?> GetPlayerByIdAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null) return null;

            return new PlayerResponseDto
            {
                Id = player.Id,
                Nickname = player.Nickname,
                Score = player.Score
            };
        }


        public async Task<PlayerResponseDto> CreatePlayerAsync(RegisterPlayerDto dto)
        {
            _logger.LogInformation($"Реєстрація нового гравця: {dto.Nickname}");
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var player = new Player
            {
                Nickname = dto.Nickname,
                PasswordHash = hashedPassword,
                Score = 0 
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return new PlayerResponseDto
            {
                Id = player.Id,
                Nickname = player.Nickname,
                Score = player.Score
            };
        }

        public async Task<bool> VerifyPassword(string nickname, string password)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Nickname == nickname);
            if (player == null) return false;

            // Перевіряємо, чи введений пароль відповідає хешу в базі
            return BCrypt.Net.BCrypt.Verify(password, player.PasswordHash);
        }

        public async Task<bool> UpdateScoreAsync(int id, int newScore)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return false;

            player.Score = newScore;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return false;

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}