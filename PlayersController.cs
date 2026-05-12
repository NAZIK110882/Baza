using Microsoft.AspNetCore.Mvc;
using Baza.Models.DTOs; // Підключаємо наші конверти-DTO
using Baza.Services;

namespace Baza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // Отримати всіх (повертаємо список DTO)
        [HttpGet]
        public async Task<ActionResult<List<PlayerResponseDto>>> GetPlayers()
        {
            var players = await _playerService.GetAllPlayersAsync();
            return Ok(players);
        }

        // Отримати одного за ID (повертаємо DTO)
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerResponseDto>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return NotFound("Гравця не знайдено в Dirty 21");
            }

            return Ok(player);
        }

        // Реєстрація (приймаємо RegisterPlayerDto)
        [HttpPost("register")]
        public async Task<ActionResult<PlayerResponseDto>> Register(RegisterPlayerDto dto)
        {
            var result = await _playerService.CreatePlayerAsync(dto);
            return Ok(result);
        }
    }
}