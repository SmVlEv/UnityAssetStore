using UnityAssetStore.Data;
using UnityAssetStore.Models;

namespace UnityAssetStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }

        public Order CreateOrder()
        {
            return new Order
            {
                OrderDate = DateTime.Now,
                Status = "Processing",
                Items = new List<OrderItem>()
            };
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
