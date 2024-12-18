using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Services
{
    public static class Mapper
    {
        public static BikePartVM? ToBikePartVM(BikePart obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new BikePartVM
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Manufacturer = obj.Manufacturer,
                Model = obj.Model,
                Material = obj.Material,
                Color = obj.Color,
                Weight = obj.Weight,
                Diameter = obj.Diameter,
                Width = obj.Width,
                Length = obj.Length,
                Depth = obj.Depth,
                LinksNumber = obj.LinksNumber,
                InstallationDiameter = obj.InstallationDiameter,
                InstallationWidth = obj.InstallationWidth,
                TypeAndPurpose = obj.TypeAndPurpose,
                Category = (CategoryVM)obj.Category
            };
        }

        public static BikePart? ToBikePart(BikePartVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new BikePart
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Manufacturer = obj.Manufacturer,
                Model = obj.Model,
                Material = obj.Material,
                Color = obj.Color,
                Weight = obj.Weight,
                Diameter = obj.Diameter,
                Width = obj.Width,
                Length = obj.Length,
                Depth = obj.Depth,
                LinksNumber = obj.LinksNumber,
                InstallationDiameter = obj.InstallationDiameter,
                InstallationWidth = obj.InstallationWidth,
                TypeAndPurpose = obj.TypeAndPurpose,
                Category = Category.BikeParts
            };
        }

        public static ServiceVM? ToServiceVM(Service obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new ServiceVM
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Category = (CategoryVM)obj.Category,
                IsComparable = obj.IsComparable
            };
        }

        public static Service? ToService(ServiceVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new Service
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Category = (Category)obj.Category,
                IsComparable = obj.IsComparable
            };
        }

        public static ServiceVM? ProductVMToServiceVM(ProductVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new ServiceVM
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Category = obj.Category,
                IsComparable = obj.IsComparable
            };
        }

        public static ProductVM? ServiceToProductVM(Service obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new ProductVM
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Category = (CategoryVM)obj.Category,
                IsComparable = obj.IsComparable
            };
        }

        public static BikePartVM? ProductVMToBikePartVM(ProductVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new BikePartVM
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Category = obj.Category,
                IsComparable = obj.IsComparable
            };
        }

        public static ProductVM? BikePartToProductVM(BikePart obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new ProductVM
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Category = (CategoryVM)obj.Category,
                IsComparable = obj.IsComparable
            };
        }

        public static ProductVM? ToProductVM(Product obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new ProductVM
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                Images = obj.Images,
                Category = (CategoryVM)obj.Category,
                IsComparable = obj.IsComparable
            };
        }

        public static Product? ToProduct(ProductVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new Product
            {
                Id = obj.Id,
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description
            };
        }

        public static CartVM? ToCartVM(Cart obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new CartVM
            {
                Id = obj.Id,
                UserId = obj.UserId,
                Items = obj.Items != null ? obj.Items.Select(ToCartItemVM).ToList() : []
            };
        }

        public static CartItemVM? ToCartItemVM(CartItem obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new CartItemVM
            {
                Id = obj.Id,
                Item = ToProductVM(obj.Product),
                Quantity = obj.Quantity
            };
        }

        public static FavoritesVM? ToFavoritesVM(Favorites obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new FavoritesVM
            {
                Id = obj.Id,
                UserId = obj.UserId,
                Products = obj.Products != null ? obj.Products.Select(ToProductVM).ToList() : []
            };
        }

        public static ComparisonVM? ToComparisonVM(Comparison obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new ComparisonVM
            {
                Id = obj.Id,
                UserId = obj.UserId,
                Products = obj.Products != null ? obj.Products.Select(ToProductVM).ToList() : []
            };
        }

        public static Order? ToOrder(OrderVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new Order
            {
                Id = obj.Id,
                UserId = obj.UserId,
                Status = (OnlineShop.Db.Models.OrderStatus)obj.Status,
                Firstname = obj.Firstname,
                Lastname = obj.Lastname,
                Middlename = obj.Middlename,
                Phone = obj.Phone,
                Email = obj.Email,
                Comment = obj.Comment,
                Created = obj.Created,
                OrderItems = obj.OrderItems != null ? obj.OrderItems.Select(ToOrderItem).ToList() : []
            };
        }

        public static OrderVM? ToOrderVM(Order obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new OrderVM
            {
                Id = obj.Id,
                Number = obj.Number,
                UserId = obj.UserId,
                Status = ToOrderStatusVM(obj.Status),
                Firstname = obj.Firstname,
                Lastname = obj.Lastname,
                Middlename = obj.Middlename,
                Phone = obj.Phone,
                Email = obj.Email,
                Comment = obj.Comment,
                Created = obj.Created,
                OrderItems = obj.OrderItems != null ? obj.OrderItems.Select(ToOrderItemVM).ToList() : []
            };
        }

        public static OrderItemVM? ToOrderItemVM(OrderItem obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new OrderItemVM
            {
                Id = obj.Id,
                Item = ToProductVM(obj.Product),
                Quantity = obj.Quantity
            };
        }

        public static OrderItem? ToOrderItem(OrderItemVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new OrderItem
            {
                Id = obj.Id,
                Product = ToProduct(obj.Item),
                Quantity = obj.Quantity
            };
        }

        public static OrderItem? CartItemToOrderItem(CartItem obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new OrderItem
            {
                Product = obj.Product,
                Quantity = obj.Quantity
            };
        }

        public static OrderStatus ToOrderStatus(OrderStatusVM obj)
        {
            return (OrderStatus)Enum.Parse(typeof(OrderStatus), obj.ToString());
        }

        public static OrderStatusVM ToOrderStatusVM(OrderStatus obj)
        {
            return (OrderStatusVM)Enum.Parse(typeof(OrderStatusVM), obj.ToString());
        }

        public static UserVM? ToUserVM(User obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new UserVM
            {
                Id = obj.Id,
                Login = obj.UserName,
                Firstname = obj.Firstname,
                Lastname = obj.Lastname,
                Middlename = obj.Middlename,
                Phone = obj.PhoneNumber,
                Address = obj.Address,
                Avatar = obj.Avatar
            };
        }

        public static User? ToUser(UserVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new User
            {
                Email = obj.Login,
                UserName = obj.Login,
                Firstname = obj.Firstname,
                Lastname = obj.Lastname,
                Middlename = obj.Middlename,
                PhoneNumber = obj.Phone,
                Address = obj.Address,
                Avatar = obj.Avatar
            };
        }

        public static RoleVM? ToRoleVM(Role obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new RoleVM
            {
                Id = obj.Id,
                Name = obj.Name,
                CanBeEdited = obj.CanBeEdited,
                CanBeDeleted = obj.CanBeDeleted
            };
        }

        public static Role? ToRole(RoleVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new Role
            {
                Name = obj.Name,
                CanBeEdited = obj.CanBeEdited,
                CanBeDeleted = obj.CanBeDeleted
            };
        }

        public static OrderVM? UserVMToOrderVM(UserVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new OrderVM
            {
                Firstname = obj.Firstname,
                Lastname = obj.Lastname,
                Middlename = obj.Middlename,
                Phone = obj.Phone,
                Email = obj.Login
            };
        }
    }
}
