using UnityAssetStore.Models;

namespace UnityAssetStore.Services
{
    public interface IAssetService
    {
        IEnumerable<Asset> GetAllAssets();
        Asset GetAssetById(int id);
        void AddAsset(Asset asset);
        void UpdateAsset(Asset asset);
        void DeleteAsset(int id);
    }
}
