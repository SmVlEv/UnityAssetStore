using Microsoft.AspNetCore.Mvc;
using UnityAssetStore.Services;

namespace UnityAssetStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var order = _orderService.CreateOrder();
            return RedirectToAction("Details", new { id = order.Id });
        }
    }
}
