using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Baza.Data;
using Baza.Models;

namespace Baza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameSessionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GameSessionController(ApplicationDbContext context)
        {
            _context = context;

            // Закинемо пару фейкових записів для краси у Свагері
            if (!_context.GameSessions.Any())
            {
                _context.GameSessions.AddRange(
                    new GameSession { Id = 1, PlayerId = 1, LevelName = "Bunker_Level_1", SurvivalTimeSeconds = 125, PlayedAt = DateTime.Now.AddDays(-1) },
                    new GameSession { Id = 2, PlayerId = 1, LevelName = "Asylum_Escape", SurvivalTimeSeconds = 45, PlayedAt = DateTime.Now }
                );
                _context.SaveChanges();
            }
        }

        // 1. GET: Отримати історію всіх ігор (для адмінки або статистики)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameSession>>> GetSessions()
        {
            return await _context.GameSessions.ToListAsync();
        }

        // 2. POST: Записати нову сесію, коли гравець помер або пройшов рівень
        [HttpPost]
        public async Task<ActionResult<GameSession>> LogSession([FromBody] GameSession session)
        {
            session.PlayedAt = DateTime.Now;
            _context.GameSessions.Add(session);
            await _context.SaveChangesAsync();
            return Ok(session);
        }
    }
}