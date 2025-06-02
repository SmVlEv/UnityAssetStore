using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
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
            if (assets == null)
            {
                throw new InvalidOperationException("Не удалось загрузить список товаров.");
            }

            return View(assets);
        }

        // GET: /Admin/Dashboard/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Admin/Dashboard/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Asset model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _assetService.AddAssetAsync(model);
            return RedirectToAction("Index");
        }

        // GET: /Admin/Dashboard/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var asset = _assetService.GetAssetById(id);
            if (asset == null) return NotFound();
            return View(asset);
        }

        // POST: /Admin/Dashboard/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Asset model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _assetService.UpdateAssetAsync(model.Id, model);
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
        public IActionResult DeleteConfirmed(int id)
        {
            _assetService.DeleteAssetAsync(id).GetAwaiter().GetResult();
            return RedirectToAction("Index");
        }

        // Вспомогательный метод для выбора категории
        private void SetCategorySelectList(int selectedCategoryId = 0)
        {
            var categories = _context.Categories.ToList();
            if (categories == null || !categories.Any())
            {
                throw new InvalidOperationException("Категории не найдены в базе данных.");
            }

            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedCategoryId);
        }
    }
}