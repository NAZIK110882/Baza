using Baza.Models.DTOs;

namespace Baza.Services
{
    public interface IPlayerService
    {
        // Отримання даних
        Task<List<PlayerResponseDto>> GetAllPlayersAsync();
        Task<PlayerResponseDto?> GetPlayerByIdAsync(int id);

        Task<PlayerResponseDto> CreatePlayerAsync(RegisterPlayerDto dto);
        Task<string?> LoginAsync(LoginDto dto); 

        Task<bool> UpdateScoreAsync(int id, int newScore); 
        Task<bool> DeletePlayerAsync(int id);           
    }
}