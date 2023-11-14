using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.RoleRepository
{
    public class RoleRepositoryImpl : IRoleRepository
    {
        private readonly MySqlDbContext _ctx;

        public RoleRepositoryImpl(MySqlDbContext context)
        {
            _ctx = context;
        }

        public async Task<string> AddAsync(Role role)
        {
            _ctx.Roles!.Add(role);
            await _ctx.SaveChangesAsync();
            return role.Id!;
        }

        public async Task DeleteAsync(string id)
        {
            var exist = _ctx.Roles!.Find(id);
            if (exist != null)
            {
                _ctx.Roles!.Remove(exist);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _ctx.Roles!.ToListAsync();
        }

        public async Task<Role> GetAsync(string id)
        {
            var role = await _ctx.Roles!.FirstOrDefaultAsync(r => r.Id == id);
            return role!;
        }

        public async Task UpdateAsync(string id, Role role)
        {
            if (id == role.Id)
            {
                var Role = _ctx.Roles!.Find(id);
                if (Role != null)
                {
                    _ctx.Roles.Update(Role);
                    await _ctx.SaveChangesAsync();
                }
            }
        }
    }
}
