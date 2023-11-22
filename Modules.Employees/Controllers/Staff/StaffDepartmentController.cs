using DataAccess.handler;
using DataAccess.Repositories.DepartmentRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Staff.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Modules.Employees.Controllers.Staff
{
    [Route("api/staff/departments")]
    [ApiController]
    [Authorize(Roles = "Admin, Manager, Staff")]
    public class StaffDepartmentController : ControllerBase
    {
        private readonly HttpContextAccessor _accessor;
        private readonly StaffDepartmentService _svc;
        private readonly JwtSecurityTokenHandler handler = new();
        

        public StaffDepartmentController(HttpContextAccessor accessor, IDepartmentRepository repo)
        {
            _accessor = accessor;
            _svc = new StaffDepartmentService(repo);
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            return Ok(await _svc.ReadAsync(GetManagerEmail()));
        }

        /**
         *  Hành vi lấy ra email của Staff
         */
        private string GetManagerEmail()
        {
            var token = handler.ReadJwtToken(
                TerusAuthorizationHandler
                    .NormalizedToken(_accessor.HttpContext!.Request.Headers.Authorization)
            );

            IEnumerable<Claim> claims = token.Claims;
            return claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
        }
    }
}
