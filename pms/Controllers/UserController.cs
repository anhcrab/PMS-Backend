using DataAccess.handler;
using DataAccess.Models;
using DataAccess.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace pms.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IHttpContextAccessor _accessor;
        private readonly JwtSecurityTokenHandler _handler = new();

        public UserController(IUserRepository users, IHttpContextAccessor accessor)
        {
            _repo = users;
            _accessor = accessor;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Director")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            // Authorizing
            var FoundUser = await _repo.GetAsync(id);
            var token = _handler.ReadJwtToken(
                TerusAuthorizationHandler
                    .NormalizedToken(_accessor.HttpContext!.Request.Headers.Authorization)
            );
            IEnumerable<Claim> claims = token.Claims;
            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if(role == "Admin" || role == "Director" || email == FoundUser!.Email)
            {
                return Ok(FoundUser);
            }

            return Forbid();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Director")]
        public async Task<IActionResult> Add([FromBody] UserModel model)
        {
            return Ok(await _repo.AddAsync(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UserModel model)
        {
            // Authorizing
            var FoundUser = await _repo.GetAsync(id);
            var token = _handler.ReadJwtToken(
                TerusAuthorizationHandler
                    .NormalizedToken(_accessor.HttpContext!.Request.Headers.Authorization)
            );
            IEnumerable<Claim> claims = token.Claims;
            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (role == "Admin" || role == "Director" || email == FoundUser!.Email)
            {
                await _repo.UpdateAsync(id, model);
                return Ok();
            }
            return Forbid();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Director")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }
    }
}
