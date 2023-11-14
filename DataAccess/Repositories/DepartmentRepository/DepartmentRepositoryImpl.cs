using DataAccess.Entities;
using DataAccess.Repositories.EmployeeRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.DepartmentRepository
{
    public class DepartmentRepositoryImpl : IDepartmentRepository
    {
        private readonly MySqlDbContext _ctx;
        private readonly IEmployeeRepository _emplyees;

        public DepartmentRepositoryImpl(MySqlDbContext ctx, IEmployeeRepository employees)
        {
            _ctx = ctx;
            _emplyees = employees;
        }

        public async Task<string> AddAsync(Department model)
        {
            var exist = _ctx.Departments!.FirstOrDefault(d  => d.Id == model.Id) == null;
            _ctx.Departments!.Add(model);
            await _ctx.SaveChangesAsync();
            return model.Id;
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _ctx.Departments!.ToListAsync();
        }

        public Task<Department> GetAsync(string id)
        {
            var department = _ctx.Departments!.FirstOrDefaultAsync(d => d.Id == id);
            return department!;
        }

        public Task UpdateAsync(string id, Department model)
        {
            throw new NotImplementedException();
        }
    }
}
