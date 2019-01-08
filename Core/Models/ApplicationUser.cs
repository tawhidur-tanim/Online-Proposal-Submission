using Microsoft.AspNetCore.Identity;

namespace ProjectFinal101.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
