namespace Baza.Models.DTOs
{
    public class CreateGameSessionDto
    {
        public int PlayerId { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public int SurvivalTimeSeconds { get; set; }
    }
}