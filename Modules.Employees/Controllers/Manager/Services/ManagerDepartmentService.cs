using DataAccess.Entities;
using DataAccess.Repositories.DepartmentRepository;
using Modules.Employees.Controllers.Manager.ViewModels.DepartmentVM;

namespace Modules.Employees.Controllers.Manager.Services
{
    public class ManagerDepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public ManagerDepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<ManagerDepartmentResponse> ReadAsync(string email)
        {
            var department = await Current(email);
            return new ManagerDepartmentResponse
            {
                Id = department.Id,
                Name = department.Name,
                Address = department.Address,
                Members = department.Members
            };
        }

        public async Task EditAsync(string email, ManagerDepartmentRequest request)
        {
            await _repo.UpdateAsync(request.Id, new Department
            {
                Id= request.Id,
                Name = request.Name,
                Address = request.Address,
                Members = request.Members
            });
        }

        public async Task<Department> Current(string email)
        {
            return (await _repo.GetAllAsync()).FirstOrDefault(d => d.Members.FirstOrDefault(e => e.User.Email == email) != null)!;
        }
    }
}
