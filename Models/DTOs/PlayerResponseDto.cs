namespace Baza.Models.DTOs
{
    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}