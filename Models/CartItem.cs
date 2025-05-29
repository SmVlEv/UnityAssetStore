namespace UnityAssetStore.Models
{
    // Models/CartItem.cs
    public class CartItem
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public int Quantity { get; set; }
        public string SessionId { get; set; } // Идентификатор корзины
    }
}
