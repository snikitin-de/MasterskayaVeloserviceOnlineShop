using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Authentication.Models
{
    public class Authorization
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Логин не может быть пустым")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        public required string Password { get; set; }
        public bool ShouldRememberMe { get; set; }
        public string? AuthReturnUrl { get; set; }
    }
}
