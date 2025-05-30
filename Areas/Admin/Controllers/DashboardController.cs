// Areas/Admin/Controllers/DashboardController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnityAssetStore.Areas.Admin.Models;
using UnityAssetStore.Models;
using UnityAssetStore.Services;

namespace UnityAssetStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IAssetService _assetService;

        public DashboardController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public IActionResult Index()
        {
            var assets = _assetService.GetAllAssets();
            return View(assets);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Asset model)
        {
            if (ModelState.IsValid)
            {
                _assetService.AddAsset(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}