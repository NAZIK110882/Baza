using System.ComponentModel.DataAnnotations;

namespace Baza.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Вкажіть нікнейм")]
        public string Nickname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Вкажіть пароль")]
        public string Password { get; set; } = string.Empty;
    }
}