using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string MetaId { get; set; } = null!;
        public Usermeta Usermeta { get; set; } = null!;
    }
}
