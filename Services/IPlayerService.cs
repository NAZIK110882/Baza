using Baza.Models.DTOs; 

namespace Baza.Services
{
    public interface IPlayerService
    {
        Task<List<PlayerResponseDto>> GetAllPlayersAsync();
        Task<PlayerResponseDto?> GetPlayerByIdAsync(int id);
        Task<PlayerResponseDto> CreatePlayerAsync(RegisterPlayerDto dto);
        Task<bool> UpdateScoreAsync(int id, int newScore);
        Task<bool> DeletePlayerAsync(int id);
    }
}