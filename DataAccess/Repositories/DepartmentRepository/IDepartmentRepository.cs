using DataAccess.Entities;

namespace DataAccess.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        public Task<List<Department>> GetAllAsync();
        public Task<Department> GetAsync(string id);
        public Task<string> AddAsync(Department model);
        public Task UpdateAsync(string id, Department model);
        public Task DeleteAsync(string id);
    }
}
