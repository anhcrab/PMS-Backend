using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class Role : IdentityRole
    {
        public string? RoleName { get; set; }
    }
}
