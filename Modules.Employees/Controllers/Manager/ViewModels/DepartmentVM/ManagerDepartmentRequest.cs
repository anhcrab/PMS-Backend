using DataAccess.Entities;

namespace Modules.Employees.Controllers.Manager.ViewModels.DepartmentVM
{
    public class ManagerDepartmentRequest
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public List<Employee> Members { get; set; } = new();
    }
}
