using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.UserRepository
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly MySqlDbContext _ctx;

        public UserRepositoryImpl(MySqlDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<string> AddAsync(UserModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            var user = _ctx.Users!.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _ctx.Users!.Remove(user);
            }
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<UserModel?>> GetAllAsync()
        {
            var result = new List<UserModel?>();
            await _ctx.Users!.ForEachAsync(u =>
            {
                result.Add(new UserModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    FirstName = u.Usermeta != null ? u.Usermeta.FirstName : "",
                    LastName = u.Usermeta != null ? u.Usermeta.LastName : "",
                    PhoneNumber = u.PhoneNumber,
                    Sex = u.Usermeta != null ? u.Usermeta.Sex : "",
                    Dob = u.Usermeta != null ? u.Usermeta.Dob.ToString() : "",
                });
            });
            return result;
        }

        public async Task<UserModel?> GetAsync(string id)
        {
            var User = await _ctx.Users!.FirstOrDefaultAsync(u => u.Id == id);
            if (User == null)
            {
                return null;
            }
            return new UserModel
            {
                Id = User.Id,
                Email = User.Email,
                UserName= User.UserName,
                FirstName= User.Usermeta != null ? User.Usermeta.FirstName : "",
                LastName= User.Usermeta != null ? User.Usermeta.LastName : "",
                PhoneNumber= User.PhoneNumber,
                Sex = User.Usermeta != null ? User.Usermeta.Sex : "",
                Dob = User.Usermeta != null ? User.Usermeta.Dob.ToString() : DateTime.Now.ToString(),
            };
        }

        public async Task UpdateAsync(string id, UserModel model)
        {
            var user = _ctx.Users!.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                if (user.Usermeta == null)
                {
                    user.Usermeta = new Usermeta
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Sex= model.Sex,
                        Dob = DateOnly.Parse(model.Dob),
                        UserId = user.Id,
                        User = user,
                    };
                    _ctx.Users!.Update(user);
                }
                else
                {
                    user.Usermeta.FirstName = model.UserName;
                    user.Usermeta.LastName = model.Email;
                    user.Usermeta.Sex = model.Sex;
                    user.Usermeta.Dob = DateOnly.Parse(model.Dob);
                    _ctx.Users!.Update(user);
                }
            }
            await _ctx.SaveChangesAsync();
        }
    }
}
