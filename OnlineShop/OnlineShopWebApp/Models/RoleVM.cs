using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class RoleVM
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Название роли не может быть пустым")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название роли должно быть от {2} до {1} символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ\s]+$", ErrorMessage = "Имя не может содержать никакие символы кроме букв и пробела")]
        public string? Name { get; set; }
        public bool CanBeDeleted { get; set; } = true;
        public bool CanBeEdited { get; set; } = true;

        public RoleVM() { }

        public RoleVM(string name)
        {
            Name = name;
        }
    }
}
