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

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.OrderByDescending(p => p.Score).ToListAsync();
        }

        
        [HttpPost("login")]
        public async Task<ActionResult<Player>> Login([FromBody] string nickname)
        {
            
            var player = await _context.Players
                .FirstOrDefaultAsync(p => p.Nickname == nickname);

            if (player == null)
            {
                player = new Player
                {
                    Nickname = nickname,
                    Score = 0
                };

                _context.Players.Add(player);
                await _context.SaveChangesAsync();
            }

            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }
    }
}