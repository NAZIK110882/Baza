using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Baza.Data;

namespace Baza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ОТРИМАТИ всіх гравців (Leaderboard)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.OrderByDescending(p => p.Score).ToListAsync();
        }

        // ДОДАТИ нового гравця або зберегти результат
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }
    }
}