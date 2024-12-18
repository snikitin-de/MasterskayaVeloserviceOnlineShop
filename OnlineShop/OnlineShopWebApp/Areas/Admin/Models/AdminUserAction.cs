using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public enum AdminUserAction
    {
        [Display(Name = "Смена пароля")]
        ChangePassword,
        [Display(Name = "Редактирование данных")]
        Edit,
        [Display(Name = "Права доступа")]
        AccessRights,
        [Display(Name = "Удалить пользователя")]
        RemoveUser
    }
}
