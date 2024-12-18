using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;

namespace OnlineShopWebApp.Services
{
    public class OrderService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly CartService _cartService;
        private readonly UserService _userService;

        public OrderService(IOrdersRepository ordersRepository, CartService cartService, UserService userService)
        {
            _ordersRepository = ordersRepository;
            _cartService = cartService;
            _userService = userService;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _ordersRepository.GetAllAsync();
        }

        public async Task<Order?> GetAsync(string id)
        {
            return await _ordersRepository.GetAsync(id);
        }

        public async Task<bool> AddAsync(string userId, Order order)
        {
            if (userId != null && order != null)
            {
                var cart = await _cartService.GetAsync(userId);

                order.Id ??= Guid.NewGuid().ToString();
                order.UserId = userId;
                order.OrderItems = cart.Items.Select(Mapper.CartItemToOrderItem).ToList();

                var isAdded = await _ordersRepository.AddAsync(order);

                if (!isAdded)
                {
                    return await _ordersRepository.UpdateAsync(order);
                }

                return isAdded;
            }

            return false;
        }

        public async Task ChangeStatusAsync(string id, OrderStatus status)
        {
            var order = await GetAsync(id);

            if (order != null)
            {
                order.Status = status;

                await _ordersRepository.UpdateAsync(order);
            }
        }
    }
}
