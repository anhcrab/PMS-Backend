using DataAccess.Models;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public Task<List<UserModel?>> GetAllAsync();
        public Task<UserModel?> GetAsync(string id);
        public Task<string> AddAsync(UserModel model);
        public Task UpdateAsync(string id, UserModel model);
        public Task DeleteAsync(string id);
    }
}
