using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UnityAssetStore.Data;
using UnityAssetStore.Models;

namespace UnityAssetStore.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Можно передать список популярных товаров или категории
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
