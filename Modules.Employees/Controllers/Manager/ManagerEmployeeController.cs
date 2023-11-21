using DataAccess.handler;
using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Manager.Services;
using Modules.Employees.Controllers.Manager.ViewModels.EmployeeVM;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Modules.Employees.Controllers.Manager
{
    [Route("api/manager/employees")]
    [ApiController]
    [Authorize(Roles = "Admin, Manager")]
    public class ManagerEmployeeController : ControllerBase
    {
        private readonly ManagerEmployeeService _svc;
        private readonly IHttpContextAccessor _accessor;
        private readonly JwtSecurityTokenHandler handler = new();

        public ManagerEmployeeController(
            IEmployeeRepository repo1,
            IDepartmentRepository repo2,
            IHttpContextAccessor accessor
        )
        {
            _svc = new ManagerEmployeeService(repo1, repo2);
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
            return Ok(await _svc.FindAll(email!));
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var token = handler.ReadJwtToken(
                TerusAuthorizationHandler
                    .NormalizedToken(_accessor.HttpContext!.Request.Headers.Authorization)
            );

            IEnumerable<Claim> claims = token.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return Ok(await _svc.FindById(id, email!));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ManagerEmployeeRequest request)
        {
            return Ok(await _svc.Add(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, ManagerEmployeeRequest request)
        {
            await _svc.Update(id, request);
            return Ok("Success");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _svc.Remove(id);
            return Ok("Success");
        }
    }
}
