using UnityAssetStore.Models;

namespace UnityAssetStore.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        Order CreateOrder();
        void AddOrder(Order order);
    }
}
