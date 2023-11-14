using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.EmployeeRepository
{
    public class EmployeeRepositoryImpl : IEmployeeRepository
    {
        private readonly MySqlDbContext _ctx;

        public EmployeeRepositoryImpl(MySqlDbContext ctx)
        {
            _ctx = ctx;
        } 

        public async Task<string> AddAsync(Employee model)
        {
            var user = _ctx.Employees!.FirstOrDefault(e => e.User.Email == model.User.Email);

            if (user == null)
            {
                _ctx.Employees!.Add(model);
                await _ctx.SaveChangesAsync();
            }
            return model.Id;
        }

        public async Task DeleteAsync(string id)
        {
            var employee = _ctx.Employees!.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _ctx.Employees!.Remove(employee);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _ctx.Employees!.ToListAsync();
        }

        public async Task<Employee> GetAsync(string id)
        {
            var e =  await _ctx.Employees!.FirstOrDefaultAsync(employee => employee.Id == id);
            return e!;
        }

        public async Task UpdateAsync(string id, Employee model)
        {
            var employee = _ctx.Employees!.FirstOrDefault(e => e.Id == id);
            if (employee != null && model.ConcurencyStamp == employee.ConcurencyStamp)
            {
                model.ConcurencyStamp = Guid.NewGuid().ToString();
                _ctx.Employees!.Update(model);
                await _ctx.SaveChangesAsync();
            }
            throw new NotImplementedException();
        }
    }
}
