using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class UserModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Sex { get; set; } = null!;
        [Required]
        public string Dob { get; set; } = null!;
    }
}
