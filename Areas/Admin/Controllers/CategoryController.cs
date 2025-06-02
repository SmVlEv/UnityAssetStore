using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnityAssetStore.Models;
using UnityAssetStore.Services;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: /Admin/Category/Index
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    // GET: /Admin/Category/Add
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // POST: /Admin/Category/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Category model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _categoryService.AddCategoryAsync(model);
        return RedirectToAction("Index");
    }

    // GET: /Admin/Category/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null) return NotFound();

        return View(category);
    }

    // POST: /Admin/Category/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Category model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _categoryService.UpdateCategoryAsync(id, model);
        return RedirectToAction("Index");
    }

    // GET: /Admin/Category/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    // POST: /Admin/Category/Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _categoryService.DeleteCategoryAsync(id).GetAwaiter().GetResult();
        return RedirectToAction("Index");
    }
}