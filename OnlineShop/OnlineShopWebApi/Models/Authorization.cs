using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApi.Models
{
    public class Authorization
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
