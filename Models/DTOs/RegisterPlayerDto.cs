using System.ComponentModel.DataAnnotations;

namespace Baza.Models.DTOs
{
    public class RegisterPlayerDto
    {
        [Required(ErrorMessage = "Нікнейм обов'язковий")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Нікнейм має бути від 3 до 20 символів")]
        public string Nickname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обов'язковий")]
        [MinLength(6, ErrorMessage = "Пароль має бути не менше 6 символів")]
        public string Password { get; set; } = string.Empty;
    }
}