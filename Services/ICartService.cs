using UnityAssetStore.Models;

namespace UnityAssetStore.Services
{
    public interface ICartService
    {
        ShoppingCart GetCart(ISession session);
        ShoppingCart AddToCart(ISession session, int assetId);
        ShoppingCart RemoveFromCart(ISession session, int assetId);
        void ClearCart(ISession session);
    }
}
