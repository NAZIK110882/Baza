using Baza.Models.DTOs; 

namespace Baza.Services
{
    public interface IPlayerService
    {
        Task<List<PlayerResponseDto>> GetAllPlayersAsync();
        Task<PlayerResponseDto?> GetPlayerByIdAsync(int id);
        Task<PlayerResponseDto> CreatePlayerAsync(RegisterPlayerDto dto);
    }
}