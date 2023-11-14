using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("departments")]
    public class Department
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Address { get; set; } = null!;

        public List<Employee> Members { get; set; } = null!; 
        // Điều kiện là mỗi 1 phòng ban thì chỉ được phép có 1 quản lí
        // nằm trong danh sách thành viên của phòng ban
    }
}
