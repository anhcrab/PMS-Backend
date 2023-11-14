using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("usermetas")]
    public class Usermeta
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string LastName { get; set; } = null!;

        public string Sex { get; set; } = "Unknown";

        public DateOnly Dob { get; set; } = DateOnly.Parse(DateTime.Now.ToShortDateString());

        [Required]
        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
