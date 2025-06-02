using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UnityAssetStore.Data;
using UnityAssetStore.Models;
using System.Linq;
using System;

namespace UnityAssetStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(int id, Category updatedCategory)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null) throw new ArgumentException("Категория не найдена", nameof(id));

            existingCategory.Name = updatedCategory.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new ArgumentException("Категория не найдена", nameof(id));

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}