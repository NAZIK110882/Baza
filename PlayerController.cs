using Microsoft.AspNetCore.Mvc;
using Baza.Models.DTOs;
using Baza.Services;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        public async Task<ActionResult<List<PlayerResponseDto>>> GetPlayers()
        {
            var players = await _playerService.GetAllPlayersAsync();
            return Ok(players);
        }

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

        [HttpPost("register")]
        public async Task<ActionResult<PlayerResponseDto>> Register([FromBody] RegisterPlayerDto dto)
        {
            var result = await _playerService.CreatePlayerAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _playerService.LoginAsync(dto);

            if (token == null)
                return Unauthorized("Невірний нікнейм або пароль");

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpPut("{id}/score")]
        public async Task<IActionResult> UpdateScore(int id, [FromBody] int newScore)
        {
            var result = await _playerService.UpdateScoreAsync(id, newScore);
            if (!result) return NotFound("Гравця не знайдено");

            return Ok(new { message = "Рахунок оновлено успішно" });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var result = await _playerService.DeletePlayerAsync(id);
            if (!result) return NotFound("Гравця не знайдено");

            return Ok(new { message = "Гравця видалено з бази Dirty 21" });
        }
    }
}