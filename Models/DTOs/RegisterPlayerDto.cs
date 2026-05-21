using System.ComponentModel.DataAnnotations;

namespace Baza.Models.DTOs
{
    public class RegisterPlayerDto
    {
        [Required(ErrorMessage = "Нікнейм обов'язковий")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Нікнейм має бути від 3 до 20 символів")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Нікнейм може містити тільки букви, цифри та підкреслення")]
        public string Nickname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обов'язковий")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Пароль має бути не менше 6 символів")]
        public string Password { get; set; } = string.Empty;
    }
}