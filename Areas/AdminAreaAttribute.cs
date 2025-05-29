// Areas/Admin/AdminAreaAttribute.cs
using Microsoft.AspNetCore.Mvc;

namespace UnityAssetStore.Areas.Admin
{
    public class AdminAreaAttribute : AreaAttribute
    {
        public AdminAreaAttribute() : base("Admin")
        {
        }
    }
}