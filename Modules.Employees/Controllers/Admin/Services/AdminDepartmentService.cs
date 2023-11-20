using DataAccess.Entities;
using DataAccess.Repositories.DepartmentRepository;
using Modules.Employees.Controllers.Admin.ViewModels.DepartmentVM;

namespace Modules.Employees.Controllers.Admin.Services
{
    public class AdminDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public AdminDepartmentService(IDepartmentRepository repo) 
        {
            _repository = repo;
        }
        public async Task<List<AdminDepartmentResponse>> FindAll()
        {
            var result = new List<AdminDepartmentResponse>();
            var list = await _repository.GetAllAsync();
            foreach (var item in list)
            {
                result.Add(new AdminDepartmentResponse
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    Members = item.Members,
                });
            }
            return result;
        }

        public async Task<AdminDepartmentResponse> FindById(string id)
        {
            var item = await _repository.GetAsync(id);
            return new AdminDepartmentResponse
            {
                Id = item.Id,
                Name = item.Name,
                Address = item.Address,
                Members = item.Members,
            };
        }

        public async Task<string> Add(AdminDepartmentRequest request)
        {
            var newDepartment = new Department
            {
                Name = request.Name,
                Address = request.Address,
                Members = request.Members,
            };
            return await _repository.AddAsync(newDepartment);
        }

        public async Task Update(string id, AdminDepartmentRequest request)
        {
            await _repository.UpdateAsync(id, new Department
            {
                Id = id,
                Name = request.Name,
                Address = request.Address,
                Members = request.Members
            });
        }

        public async Task Remove(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
