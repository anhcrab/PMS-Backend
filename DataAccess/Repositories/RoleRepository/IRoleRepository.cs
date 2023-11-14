using DataAccess.Entities;

namespace DataAccess.Repositories.RoleRepository
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllAsync();
        public Task<Role> GetAsync(string id);
        public Task<string> AddAsync(Role role);
        public Task UpdateAsync(string id, Role role);
        public Task DeleteAsync(string id);
    }
}
