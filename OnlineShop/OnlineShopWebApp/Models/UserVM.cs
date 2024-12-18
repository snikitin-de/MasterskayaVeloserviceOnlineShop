using OnlineShop.Db.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserVM
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Логин не может быть пустым")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [RegularExpression(@"^(?=.*[0-9].*)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*-_]{8,25}$", ErrorMessage = "Пароль должен быть от 8 до 25 символов и содержать как минимум одну цифру, одну латинскую букву в нижнем и верхнем регистре. Доступные спец. символы: !@#$%^&*-_")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string? ConfirmedPassword { get; set; }

        [StringLength(50, ErrorMessage = "Имя не должно быть длиннее 50 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ’`'\-\s]+$", ErrorMessage = "Имя не может содержать никакие символы кроме букв, пробела и знаков \"’\", \"`\", \"'\", \"-\"")]
        public string? Firstname { get; set; }

        [StringLength(50, ErrorMessage = "Фамилия не должно быть длиннее 50 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ’`'\-\s]+$", ErrorMessage = "Фамилия не может содержать никакие символы кроме букв, пробела и знаков \"’\", \"`\", \"'\", \"-\"")]
        public string? Lastname { get; set; }

        [StringLength(50, ErrorMessage = "Отчество не должно быть длиннее 50 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ’`'\-\s]+$", ErrorMessage = "Отчество не может содержать никакие символы кроме букв, пробела и знаков \"’\", \"`\", \"'\", \"-\"")]
        public string? Middlename { get; set; }

        [RegularExpression(@"^\+7[0-9]{3}[0-9]{3}[0-9]{2}[0-9]{2}$", ErrorMessage = "Телефон должен быть в формате +70000000000")]
        public string? Phone { get; set; }

        [StringLength(500, ErrorMessage = "Адрес не должен быть длиннее 500 символов")]
        public string? Address { get; set; }
        public Image? Avatar { get; set; }
        public IFormFile? UploadedFile { get; set; }
        public RoleVM? Role { get; set; }
        public CartVM? Cart { get; set; }
        public List<OrderVM>? Orders { get; set; }

        public UserVM() { }

        public UserVM(string login)
        {
            Login = login;
            Password = string.Empty;
            Cart = new CartVM();
            Orders = [];
        }

        public UserVM(string login, string password) : this(login)
        {
            Password = password;
        }
    }
}
