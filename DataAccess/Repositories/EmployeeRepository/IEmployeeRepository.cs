using DataAccess.Entities;

namespace DataAccess.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetAllAsync();
        public Task<Employee> GetAsync(string id);
        public Task<string> AddAsync(Employee model);
        public Task UpdateAsync(string id, Employee model);
        public Task DeleteAsync(string id);
    }
}
