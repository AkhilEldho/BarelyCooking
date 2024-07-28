using Microsoft.AspNetCore.Identity;

namespace Barely_Cooking_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
