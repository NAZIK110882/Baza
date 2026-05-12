using Baza.Data;
using Baza.Models;
using Baza.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;       
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Baza.Models.Entities;

namespace Baza.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlayerService> _logger;
        private readonly IConfiguration _configuration; 

        public PlayerService(ApplicationDbContext context, ILogger<PlayerService> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration; 
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Nickname == dto.Nickname);

            if (player == null || !BCrypt.Net.BCrypt.Verify(dto.Password, player.PasswordHash))
                return null;

            var keyString = _configuration["Jwt:Key"]; 
            if (string.IsNullOrEmpty(keyString)) return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, player.Nickname),
                new Claim(ClaimTypes.NameIdentifier, player.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<PlayerResponseDto>> GetAllPlayersAsync()
        {
            return await _context.Players
                .Select(p => new PlayerResponseDto { Id = p.Id, Nickname = p.Nickname, Score = p.Score })
                .ToListAsync();
        }

        public async Task<PlayerResponseDto?> GetPlayerByIdAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return null;
            return new PlayerResponseDto { Id = player.Id, Nickname = player.Nickname, Score = player.Score };
        }

        public async Task<PlayerResponseDto> CreatePlayerAsync(RegisterPlayerDto dto)
        {
            var player = new Player
            {
                Nickname = dto.Nickname,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Player {player.Nickname} registered.");

            return new PlayerResponseDto { Id = player.Id, Nickname = player.Nickname, Score = player.Score };
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