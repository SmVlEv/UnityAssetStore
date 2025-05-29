using Microsoft.AspNetCore.Identity;

namespace UnityAssetStore.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // Здесь можно добавить дополнительные поля
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
