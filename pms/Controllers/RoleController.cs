using DataAccess.Entities;
using DataAccess.handler;
using DataAccess.Repositories.RoleRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace pms.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleRepository _repository;
        private readonly IHttpContextAccessor _accessor;
        private readonly JwtSecurityTokenHandler handler = new();

        public RoleController(IRoleRepository repo,IHttpContextAccessor accessor) 
        {
            _repository = repo;
            _accessor = accessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var token = handler.ReadJwtToken(
                TerusAuthorizationHandler
                    .NormalizedToken(_accessor.HttpContext!.Request.Headers.Authorization)
            );

            IEnumerable<Claim> claims = token.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var role = await _repository.GetAsync(id);
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Role role)
        {
            await _repository.AddAsync(role);
            return Ok();
        }
    }
}
