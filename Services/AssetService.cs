using UnityAssetStore.Data;
using UnityAssetStore.Models;

namespace UnityAssetStore.Services
{
    public class AssetService : IAssetService
    {
        private readonly AppDbContext _context;

        public AssetService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Asset> GetAllAssets()
        {
            return _context.Assets.ToList();
        }

        public Asset GetAssetById(int id)
        {
            return _context.Assets.Find(id);
        }

        public void AddAsset(Asset asset)
        {
            _context.Assets.Add(asset);
            _context.SaveChanges();
        }

        public void UpdateAsset(Asset asset)
        {
            _context.Assets.Update(asset);
            _context.SaveChanges();
        }

        public void DeleteAsset(int id)
        {
            var asset = _context.Assets.Find(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                _context.SaveChanges();
            }
        }
    }
}
