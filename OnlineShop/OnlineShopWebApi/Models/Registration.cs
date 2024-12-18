using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApi.Models
{
    public class Registration
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
