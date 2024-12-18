using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class OrderVM
    {
        public string? Id { get; set; }
        public int Number { get; set; }
        public string? UserId { get; set; }
        public OrderStatusVM Status { get; set; } = OrderStatusVM.Created;

        [Required(ErrorMessage = "Имя не может быть пустым")]
        [StringLength(50, ErrorMessage = "Имя не должно быть длиннее 50 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ’`'\-\s]+$", ErrorMessage = "Имя не может содержать никакие символы кроме букв, пробела и знаков \"’\", \"`\", \"'\", \"-\"")]
        public required string Firstname { get; set; }

        [Required(ErrorMessage = "Фамилия не может быть пустой")]
        [StringLength(50, ErrorMessage = "Фамилия не должно быть длиннее 50 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ’`'\-\s]+$", ErrorMessage = "Фамилия не может содержать никакие символы кроме букв, пробела и знаков \"’\", \"`\", \"'\", \"-\"")]
        public required string Lastname { get; set; }

        [StringLength(50, ErrorMessage = "Отчество не должно быть длиннее 50 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ’`'\-\s]+$", ErrorMessage = "Отчество не может содержать никакие символы кроме букв, пробела и знаков \"’\", \"`\", \"'\", \"-\"")]
        public string? Middlename { get; set; }

        [Required(ErrorMessage = "Телефон не может быть пустым")]
        [RegularExpression(@"^\+7[0-9]{3}[0-9]{3}[0-9]{2}[0-9]{2}$", ErrorMessage = "Телефон должен быть в формате +70000000000")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Email не может быть пустым")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public required string Email { get; set; }
        public string? Comment { get; set; }

        public DateTime Created { get; set; } = DateTime.Now.ToUniversalTime();
        public List<OrderItemVM> OrderItems { get; set; } = [];
        public decimal Sum
        {
            get => OrderItems.Sum(x => x.Sum);
        }
    }
}
