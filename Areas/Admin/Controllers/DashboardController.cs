using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnityAssetStore.Areas.Admin.Models;
using UnityAssetStore.Data;
using UnityAssetStore.Models;
using UnityAssetStore.Services;

namespace UnityAssetStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly AppDbContext _context;

        public DashboardController(IAssetService assetService, AppDbContext context)
        {
            _assetService = assetService;
            _context = context;
        }

        // GET: /Admin/Dashboard/Index
        public IActionResult Index()
        {
            var assets = _assetService.GetAllAssets();
            return View(assets);
        }

        // GET: /Admin/Dashboard/Add
        [HttpGet]
        public IActionResult Add()
        {
            SetCategorySelectList();
            return View();
        }

        // POST: /Admin/Dashboard/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Asset model)
        {
            if (!ModelState.IsValid)
            {
                SetCategorySelectList();
                return View(model);
            }

            _assetService.AddAsset(model);
            return RedirectToAction("Index");
        }

        // GET: /Admin/Dashboard/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var asset = _assetService.GetAssetById(id);
            if (asset == null) return NotFound();

            SetCategorySelectList();
            return View(asset);
        }

        // POST: /Admin/Dashboard/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Asset model)
        {
            if (!ModelState.IsValid)
            {
                SetCategorySelectList();
                return View(model);
            }

            _assetService.UpdateAsset(model);
            return RedirectToAction("Index");
        }

        // GET: /Admin/Dashboard/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var asset = _assetService.GetAssetById(id);
            if (asset == null) return NotFound();

            return View(asset);
        }

        // POST: /Admin/Dashboard/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Asset model)
        {
            _assetService.DeleteAsset(model.Id);
            return RedirectToAction("Index");
        }

        // Вспомогательный метод для выбора категории
        private void SetCategorySelectList()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}