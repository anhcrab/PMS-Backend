using DataAccess.Entities;

namespace Modules.Employees.Controllers.Staff.ViewModels.DepartmentVM
{
    public class StaffDepartmentResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public List<Employee> Members { get; set; } = new List<Employee>();
    }
}
