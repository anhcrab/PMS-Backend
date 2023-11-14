using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Hometown { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Position { get; set; } = null!;

        [Required]
        public string ConcurencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string SupervisorId { get; set; } = null!;

        [Required]
        public string DepartmentId { get; set; } = null!;

        public User User { get; set; } = null!;

        public Employee Supervisor { get; set; } = null!;

        public List<Employee>? TeamMembers { get; set; } = new List<Employee>();

        public Department Department { get; set; } = null!;

    }
}
