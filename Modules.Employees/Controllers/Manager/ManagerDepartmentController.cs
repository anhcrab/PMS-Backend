using DataAccess.handler;
using DataAccess.Repositories.DepartmentRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Manager.Services;
using Modules.Employees.Controllers.Manager.ViewModels.DepartmentVM;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Modules.Employees.Controllers.Manager
{
    [Route("api/manager/departments")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerDepartmentController : ControllerBase
    {
        private readonly ManagerDepartmentService _svc;
        private readonly HttpContextAccessor _accessor;
        private readonly JwtSecurityTokenHandler handler = new();

        public ManagerDepartmentController(IDepartmentRepository repo, HttpContextAccessor accessor)
        {
            _svc = new ManagerDepartmentService(repo);
            _accessor = accessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _svc.ReadAsync(GetManagerEmail()));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ManagerDepartmentRequest request)
        {
            await _svc.EditAsync(GetManagerEmail(), request);
            return Ok("Success");
        }

        /**
         *  Hành vi lấy ra email của Manager
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
