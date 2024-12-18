using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Authentication.Models
{
    public class Registration
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Логин не может быть пустым")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [RegularExpression(@"^(?=.*[0-9].*)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*-_]{8,25}$", ErrorMessage = "Пароль должен быть от 8 до 25 символов и содержать как минимум одну цифру, одну латинскую букву в нижнем и верхнем регистре. Доступные спец. символы: !@#$%^&*-_")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public required string ConfirmedPassword { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
