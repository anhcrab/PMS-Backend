using System.ComponentModel.DataAnnotations;

namespace pms.Services.Models.Authentication.SignIn
{
    public class SignIn
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
