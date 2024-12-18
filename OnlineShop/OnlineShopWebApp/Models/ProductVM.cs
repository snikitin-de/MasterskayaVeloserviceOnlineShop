using OnlineShop.Db.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ProductVM
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Наименование не может быть пустым")]
        [StringLength(100, ErrorMessage = "Наименование не должно быть длиннее 100 символов")]
        public required string Name { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена не может быть пустой")]
        [Range(0, int.MaxValue, ErrorMessage = $"Цена не может быть отрицательной")]
        public required decimal Price { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Описание не может быть пустым")]
        public required string Description { get; set; }
        public List<Image> Images { get; set; }
        public IEnumerable<IFormFile> UploadedFiles { get; set; } = [];
        public CategoryVM Category { get; set; }
        public bool IsComparable { get; set; }

        public ProductVM() { }

        public ProductVM(string name, decimal price, string description, List<Image> images, CategoryVM category, bool isComparable)
        {
            Name = name;
            Price = price;
            Description = description;
            Images = images;
            Category = category;
            IsComparable = isComparable;
        }
    }
}
