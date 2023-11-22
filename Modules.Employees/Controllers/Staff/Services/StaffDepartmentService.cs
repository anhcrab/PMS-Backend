using DataAccess.Entities;
using DataAccess.Repositories.DepartmentRepository;
using Modules.Employees.Controllers.Staff.ViewModels.DepartmentVM;

namespace Modules.Employees.Controllers.Staff.Services
{
    public class StaffDepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public StaffDepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<StaffDepartmentResponse> ReadAsync(string email)
        {
            var department = await Current(email);
            return new StaffDepartmentResponse
            {
                Id = department.Id,
                Name = department.Name,
                Address = department.Address,
                Members = department.Members
            };
        }

        public async Task<Department> Current(string email)
        {
            return (await _repo.GetAllAsync()).FirstOrDefault(d => d.Members.FirstOrDefault(e => e.User.Email == email) != null)!;
        }
    }
}
