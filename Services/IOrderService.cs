using UnityAssetStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityAssetStore.Services
{
    public interface IOrderService
    {
        // Получение всех заказов (например, для админки)
        IEnumerable<Order> GetAllOrders();

        // Получение заказов пользователя
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);

        // Получение одного заказа по ID
        Task<Order?> GetOrderByIdAsync(int id);

        // Создание заказа из корзины
        Task<int> CreateOrderFromCartAsync(string userId);
    }
}