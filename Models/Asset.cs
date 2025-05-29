using System.ComponentModel.DataAnnotations;

namespace UnityAssetStore.Models
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        public string PreviewImageUrl { get; set; } // URL или путь к изображению

        public string FilePath { get; set; } // Путь к .unitypackage файлу

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
