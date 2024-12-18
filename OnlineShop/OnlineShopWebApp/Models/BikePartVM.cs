using OnlineShop.Db.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class BikePartVM : ProductVM
    {
        [Display(Name = "Производитель")]
        [StringLength(100, ErrorMessage = "Производитель не может быть длиннее 100 символов")]
        public string? Manufacturer { get; set; }

        [Display(Name = "Модель")]
        [StringLength(100, ErrorMessage = "Модель не может быть длиннее 100 символов")]
        public string? Model { get; set; }

        [Display(Name = "Материал")]
        [StringLength(50, ErrorMessage = "Материал не может быть длиннее 50 символов")]
        public string? Material { get; set; }

        [Display(Name = "Цвет")]
        [StringLength(50, ErrorMessage = "Цвет не может быть длиннее 50 символов")]
        public string? Color { get; set; }

        [Display(Name = "Вес (грамм)")]
        [Range(0, 100000.0f, ErrorMessage = "Вес должен быть в пределах от {1} до {2} грамм")]
        public float? Weight { get; set; }

        [Display(Name = "Диаметр (миллиметр)")]
        [Range(0, 1000.0f, ErrorMessage = "Диаметр должен быть в пределах от {1} до {2} миллиметров")]
        public float? Diameter { get; set; }

        [Display(Name = "Ширина (миллиметр)")]
        [Range(0, 1000.0f, ErrorMessage = "Ширина должна быть в пределах от {1} до {2} миллиметров")]
        public float? Width { get; set; }

        [Display(Name = "Длина (миллиметр)")]
        [Range(0, 1000.0f, ErrorMessage = "Длина должна быть в пределах от {1} до {2} миллиметров")]
        public float? Length { get; set; }

        [Display(Name = "Глубина (миллиметр)")]
        [Range(0, 1000.0f, ErrorMessage = "Глубина должна быть в пределах от {1} до {2} миллиметров")]
        public float? Depth { get; set; }

        [Display(Name = "Количество звеньев (штука)")]
        [Range(0, 200, ErrorMessage = "Количество звеньев должно быть в пределах от {1} до {2} штук")]
        public int? LinksNumber { get; set; }

        [Display(Name = "Установочная ширина (миллиметр)")]
        [Range(0, 1000.0f, ErrorMessage = "Установочная ширина должна быть в пределах от {1} до {2} миллиметров")]
        public float? InstallationWidth { get; set; }

        [Display(Name = "Установочный диаметр (миллиметр)")]
        [Range(0, 1000.0f, ErrorMessage = "Установочный диаметр должен быть в пределах от {1} до {2} миллиметров")]
        public float? InstallationDiameter { get; set; }

        [Display(Name = "Тип и назначение")]
        [StringLength(250, ErrorMessage = "Тип и назначение не может быть длиннее 250 символов")]
        public string? TypeAndPurpose { get; set; }

        public BikePartsCategories PartCategory { get; set; }

        public BikePartVM() { }

        public BikePartVM(
            string name,
            decimal price,
            string description,
            List<Image> images,
            string manufacturer,
            string model,
            string material,
            string color,
            float weight,
            float diameter,
            float width,
            float depth,
            float length,
            int linksNumber,
            float installationWidth,
            float installationDiameter,
            string typeAndPurpose,
            BikePartsCategories partCategory) :
            base(name, price, description, images, CategoryVM.BikeParts, true)
        {
            PartCategory = partCategory;
            Manufacturer = manufacturer;
            Model = model;
            Material = material;
            Color = color;
            Weight = weight;
            Depth = depth;
            Length = length;
            LinksNumber = linksNumber;
            InstallationWidth = installationWidth;
            InstallationDiameter = installationDiameter;
            TypeAndPurpose = typeAndPurpose;
        }
    }
}
