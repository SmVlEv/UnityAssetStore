// Areas/Admin/Controllers/DashboardController.cs
using Microsoft.AspNetCore.Mvc;
using UnityAssetStore.Areas.Admin.Models;

namespace UnityAssetStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalAssets = 100, // Здесь можно подключиться к БД
                TotalOrders = 50,
                TotalUsers = 20
            };

            return View(model);
        }
    }
}