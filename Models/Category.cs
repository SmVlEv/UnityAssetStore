using System.ComponentModel.DataAnnotations;

namespace UnityAssetStore.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}
