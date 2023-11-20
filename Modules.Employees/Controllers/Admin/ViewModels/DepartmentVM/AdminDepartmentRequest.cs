using DataAccess.Entities;

namespace Modules.Employees.Controllers.Admin.ViewModels.DepartmentVM
{
    public class AdminDepartmentRequest
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;    
        public List<Employee> Members { get; set; } = new List<Employee>();
    }
}
