using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class ServiceVM : ProductVM
    {
        public ServiceVM() { }

        public ServiceVM(string name, decimal price, string description, List<Image> images) :
            base(name, price, description, images, CategoryVM.Services, false)
        {
        }
    }
}
