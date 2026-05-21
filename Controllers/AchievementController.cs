using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Baza.Data;
using Baza.Models;

namespace Baza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AchievementController(ApplicationDbContext context)
        {
            _context = context;

            if (!_context.Achievements.Any())
            {
                _context.Achievements.AddRange(
                    new Achievement { Id = 1, PlayerId = 1, Title = "First Blood", Description = "Програти вперше", UnlockedAt = DateTime.Now.AddDays(-2) },
                    new Achievement { Id = 2, PlayerId = 1, Title = "Survivor", Description = "Вижити більше 2 хвилин", UnlockedAt = DateTime.Now }
                );
                _context.SaveChanges();
            }
        }

        [HttpGet("player/{playerId}")]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetPlayerAchievements(int playerId)
        {
            var achievements = await _context.Achievements
                .Where(a => a.PlayerId == playerId)
                .ToListAsync();
            return Ok(achievements);
        }

        [HttpPost]
        public async Task<ActionResult<Achievement>> UnlockAchievement([FromBody] Achievement achievement)
        {
            achievement.UnlockedAt = DateTime.Now;
            _context.Achievements.Add(achievement);
            await _context.SaveChangesAsync();
            return Ok(achievement);
        }
    }
}