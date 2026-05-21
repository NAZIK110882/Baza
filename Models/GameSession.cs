namespace Baza.Models
{
    public class GameSession
    {
        public int Id { get; set; }
        public int PlayerId { get; set; } 
        public string LevelName { get; set; } = string.Empty; 
        public int SurvivalTimeSeconds { get; set; } 
        public DateTime PlayedAt { get; set; }
    }
}