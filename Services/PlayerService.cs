using Baza.Data;
using Baza.Models.Entities;
using Baza.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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
                .Select(p => new PlayerResponseDto
                {
                    Id = p.Id,
                    Nickname = p.Nickname,
                    Score = p.Score
                }).ToListAsync();
        }

        public async Task<PlayerResponseDto?> GetPlayerByIdAsync(int id)
        {
            var p = await _context.Players.FindAsync(id);
            if (p == null) return null;

            return new PlayerResponseDto
            {
                Id = p.Id,
                Nickname = p.Nickname,
                Score = p.Score
            };
        }

        public async Task<PlayerResponseDto> CreatePlayerAsync(RegisterPlayerDto dto)
        {
            var player = new Player
            {
                Nickname = dto.Nickname,
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
    }
}