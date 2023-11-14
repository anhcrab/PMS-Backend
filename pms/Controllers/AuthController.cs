using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pms.Services.Models.Authentication.SignIn;
using pms.Services.Models.Authentication.SignUp;
using pms.Services.Services.AuthService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pms.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userMng;
        private readonly IConfiguration _conf;
        private readonly ISignUpService _signUpSvc;

        public AuthController(
            UserManager<User> userMng,
            IConfiguration configuration,
            ISignUpService userService
        ) 
        {
            _userMng = userMng;
            _conf = configuration;
            _signUpSvc = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> SignUp([FromBody] SignUp signUp)
        {
            var response = await _signUpSvc.CreateUserAsync(signUp, _conf["AdminEmail"]);
            return StatusCode(response.StatusCode, new
            {
                Success = response.IsSuccess,
                message = response.Message,
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SignIn ([FromBody] SignIn signIn)
        {
            var user = await _userMng.FindByEmailAsync(signIn.Email);
            if(user != null && await _userMng.CheckPasswordAsync(user, signIn.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, signIn.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var userRoles = await _userMng.GetRolesAsync(user);
                foreach (string? role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
        }

        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword()
        //{
        //    // TODO: Xử lý hành vi quên mật khẩu khi đăng nhập vào hệ thống
        //    return Ok();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword()
        //{
        //    // TODO: Xử lý hành vi thay đổi mật khẩu của chỉnh user
        //    return Ok();
        //}

        private JwtSecurityToken GetToken (List<Claim> authClaims)
        {
            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _conf["JWT:ValidIssuer"],
                audience: _conf["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
