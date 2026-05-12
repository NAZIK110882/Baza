namespace Baza.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public int Score { get; set; }
        public string? PasswordHash { get; set; }
    }
}