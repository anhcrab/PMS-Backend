using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace Modules.Employees.Controllers.Staff.ViewModels.EmployeeVM
{
    public class StaffEmployeeResponse
    {
        public string Id { get; set; } = null!;
        public string Hometown { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string ConcurencyStamp { get; set; } = null!;
        public User User { get; set; } = null!;
        public Employee Supervisor { get; set; } = null!;
        public List<Employee>? TeamMembers { get; set; } = new List<Employee>();
        public Department Department { get; set; } = null!;
    }
}
