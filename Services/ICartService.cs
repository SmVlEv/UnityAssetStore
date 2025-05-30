using Microsoft.AspNetCore.Http;
using UnityAssetStore.Models;

namespace UnityAssetStore.Services
{
    public interface ICartService
    {
        // Работа с Session (для гостей)
        ShoppingCart GetCart(ISession session);
        ShoppingCart AddToCart(ISession session, int assetId);
        ShoppingCart RemoveFromCart(ISession session, int assetId);
        void ClearCart(ISession session);

        // Работа с UserId (для зарегистрированных пользователей)
        void AddToCart(string userId, int assetId);
        void RemoveFromCart(string userId, int assetId);
        IEnumerable<CartItem> GetCartItems(string userId);
    }
}