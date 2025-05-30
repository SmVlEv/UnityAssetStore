using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cartItems = _cartService.GetCartItems(userId);
                return View(cartItems);
            }
            else
            {
                var cart = _cartService.GetCart(HttpContext.Session);
                return View(cart);
            }
        }

        [HttpPost]
        public IActionResult AddToCart(int assetId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _cartService.AddToCart(userId, assetId);
            }
            else
            {
                _cartService.AddToCart(HttpContext.Session, assetId);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int assetId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _cartService.RemoveFromCart(userId, assetId);
            }
            else
            {
                _cartService.RemoveFromCart(HttpContext.Session, assetId);
            }

            return RedirectToAction("Index");
        }
    }
}