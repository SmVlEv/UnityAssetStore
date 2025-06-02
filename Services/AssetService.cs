using Microsoft.EntityFrameworkCore;
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
            return _context.Assets
                .Include(a => a.Category)
                .ToList();
        }

        public Asset GetAssetById(int id)
        {
            return _context.Assets
                .Include(a => a.Category)
                .FirstOrDefault(a => a.Id == id);
        }

        public async Task AddAssetAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssetAsync(int id, Asset updatedAsset)
        {
            var existingAsset = await _context.Assets.FindAsync(id);
            if (existingAsset == null) throw new ArgumentException("Товар не найден", nameof(id));

            existingAsset.Name = updatedAsset.Name;
            existingAsset.Description = updatedAsset.Description;
            existingAsset.Price = updatedAsset.Price;
            existingAsset.PreviewImageUrl = updatedAsset.PreviewImageUrl;
            existingAsset.CategoryId = updatedAsset.CategoryId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssetAsync(int id)
        {
            try
            {
                var asset = await _context.Assets.FindAsync(id);
                if (asset == null)
                {
                    throw new ArgumentException("Товар не найден", nameof(id));
                }

                _context.Assets.Remove(asset);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Обработка ошибки целостности базы данных
                throw new InvalidOperationException("Не удалось удалить товар. Возможно, он используется в других записях.", ex);
            }
        }
    }
}
