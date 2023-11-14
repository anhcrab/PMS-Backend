using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using pms.Services.Model;
using pms.Services.Models.Authentication.SignUp;

namespace pms.Services.Services.AuthService
{
    public class SignUpServiceImpl : ISignUpService
    {
        private readonly RoleManager<Role> _roleMng;
        private readonly UserManager<User> _usrMng;

        public SignUpServiceImpl(
            RoleManager<Role> roleMng,
            UserManager<User> usrMng
        )
        {
            _roleMng = roleMng;
            _usrMng = usrMng;
        }

        public Task<ApiResponse<List<string>>> AsignRoleToUserAsync(IEnumerable<string> role)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<string>> CreateUserAsync(SignUp payload, string adminEmail)
        {
            // Check User Exist
            var exist = await _usrMng.FindByEmailAsync(payload.Email) != null;
            if (exist)
            {
                return new ApiResponse<string> 
                { 
                    IsSuccess = false, 
                    StatusCode = 403, 
                    Message = "User already exists!" 
                };
            }
            // Add the User to DB
            var newUser = new User
            {
                Email = payload.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = payload.Username
            };

            

            if (await _roleMng.RoleExistsAsync("Client"))
            {
                var result = await _usrMng.CreateAsync(newUser, payload.Password);
                if (!result.Succeeded)
                {
                    return new ApiResponse<string>
                    {
                        IsSuccess = false,
                        StatusCode = 500,
                        Message = "User Failed to Create"
                    };
                }
                // Add role to user
                if (newUser.Email == adminEmail && await _roleMng.RoleExistsAsync("Admin"))
                {
                    await _usrMng.AddToRoleAsync(newUser, "Admin");
                }
                else
                {
                    await _usrMng.AddToRoleAsync(newUser, "Client");
                }
                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "User created successfully",
                    Response = newUser.Id
                };
            }
            else
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = "Not Support account for client"
                };
            }
        }
    }
}
