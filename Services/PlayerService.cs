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

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}