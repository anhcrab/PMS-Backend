using pms.Services.Models.Authentication.SignUp;
using pms.Services.Model;

namespace pms.Services.Services.AuthService
{
    public interface ISignUpService
    {
        public Task<ApiResponse<string>> CreateUserAsync(SignUp payload, string adminEmail);
        public Task<ApiResponse<List<string>>> AsignRoleToUserAsync(IEnumerable<string> role);
    }
}
