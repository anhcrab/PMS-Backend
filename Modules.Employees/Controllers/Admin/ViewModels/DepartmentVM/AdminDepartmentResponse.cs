using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace Modules.Employees.Controllers.Admin.ViewModels.DepartmentVM
{
    public class AdminDepartmentResponse
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public List<Employee> Members { get; set; } = null!;
    }
}
