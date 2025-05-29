using UnityAssetStore.Models;

namespace UnityAssetStore.Services
{
    public class CartService : ICartService
    {
        private const string SessionKey = "ShoppingCart";

        public ShoppingCart GetCart(ISession session)
        {
            var cartJson = session.GetString(SessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new ShoppingCart();
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
        }

        public ShoppingCart AddToCart(ISession session, int assetId)
        {
            var cart = GetCart(session);

            var item = cart.Items.FirstOrDefault(i => i.AssetId == assetId);
            if (item == null)
            {
                cart.Items.Add(new CartItem { AssetId = assetId, Quantity = 1 });
            }
            else
            {
                item.Quantity++;
            }

            SaveCart(session, cart);
            return cart;
        }

        public ShoppingCart RemoveFromCart(ISession session, int assetId)
        {
            var cart = GetCart(session);
            var item = cart.Items.FirstOrDefault(i => i.AssetId == assetId);

            if (item != null)
            {
                cart.Items.Remove(item);
                SaveCart(session, cart);
            }

            return cart;
        }

        public void ClearCart(ISession session)
        {
            session.Remove(SessionKey);
        }

        private void SaveCart(ISession session, ShoppingCart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(SessionKey, cartJson);
        }
    }
}
