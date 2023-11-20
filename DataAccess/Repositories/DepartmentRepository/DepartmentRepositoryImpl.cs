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

        public async Task DeleteAsync(string id)
        {
            var department = _ctx.Departments!.FirstOrDefault(d => d.Id == id);
            if (department != null)
            {
                _ctx.Departments!.Remove(department);
                await _ctx.SaveChangesAsync();
            }
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

        public async Task UpdateAsync(string id, Department model)
        {
            var department = _ctx.Departments!.FirstOrDefault(e => e.Id == id);
            if (department != null)
            {
                _ctx.Departments!.Update(model);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
