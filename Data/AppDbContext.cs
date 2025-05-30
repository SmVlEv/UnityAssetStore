using Microsoft.EntityFrameworkCore;
using UnityAssetStore.Models;

namespace UnityAssetStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Seed данных: категории по умолчанию
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "3D Models" },
                new Category { Id = 2, Name = "Scripts" },
                new Category { Id = 3, Name = "Textures" },
                new Category { Id = 4, Name = "Sound Effects" },
                new Category { Id = 5, Name = "UI Kits" }
            );
            modelBuilder.Entity<CartItem>()
        .HasOne(c => c.Asset)
        .WithMany()
        .HasForeignKey(c => c.AssetId);

            // 🔹 Seed данных: товары по умолчанию
            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    Id = 1,
                    Name = "Low Poly Forest Pack",
                    Description = "Набор низкополигональных деревьев и кустов для Unity.",
                    Price = 29.99m,
                    PreviewImageUrl = "/images/forest_pack.jpg",
                    FilePath = "/uploads/assets/low_poly_forest.unitypackage",
                    CategoryId = 1
                },
                new Asset
                {
                    Id = 2,
                    Name = "Cinemachine Utilities",
                    Description = "Плагин Cinemachine для создания динамичных камер в Unity.",
                    Price = 19.99m,
                    PreviewImageUrl = "/images/cinemachine.jpg",
                    FilePath = "/uploads/assets/cinemachine_utilities.unitypackage",
                    CategoryId = 2
                },
                new Asset
                {
                    Id = 3,
                    Name = "Desert Terrain Textures",
                    Description = "Высококачественные текстуры пустыни для Unity Terrains.",
                    Price = 14.99m,
                    PreviewImageUrl = "/images/desert_textures.jpg",
                    FilePath = "/uploads/assets/desert_textures.unitypackage",
                    CategoryId = 3
                }
            );

            // 🔹 Настройка связей один-ко-многим
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Assets)
                .HasForeignKey(a => a.CategoryId);

            // 🔹 Настройка связей один-ко-многим (Order - OrderItem)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Asset)
                .WithMany()
                .HasForeignKey(oi => oi.AssetId);
        }
    }
}