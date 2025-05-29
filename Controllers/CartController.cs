using Microsoft.AspNetCore.Mvc;
using UnityAssetStore.Services;

namespace UnityAssetStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart(HttpContext.Session);
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int assetId)
        {
            var cart = _cartService.AddToCart(HttpContext.Session, assetId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int assetId)
        {
            var cart = _cartService.RemoveFromCart(HttpContext.Session, assetId);
            return RedirectToAction("Index");
        }
    }
}
