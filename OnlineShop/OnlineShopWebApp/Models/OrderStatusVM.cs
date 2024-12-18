using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public enum OrderStatusVM
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "Обработан")]
        Processed,
        [Display(Name = "В пути")]
        InTransit,
        [Display(Name = "Отменен")]
        Cancelled,
        [Display(Name = "Доставлен")]
        Delivered
    }
}
