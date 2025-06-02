using UnityAssetStore.Models;

namespace UnityAssetStore.Services
{
    public interface IAssetService
    {
        Task AddAssetAsync(Asset asset);
        Task UpdateAssetAsync(int id, Asset updatedAsset);
        Task DeleteAssetAsync(int id);
        IEnumerable<Asset> GetAllAssets();
        Asset GetAssetById(int id);
    }
}
