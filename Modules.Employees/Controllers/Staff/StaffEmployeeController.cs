using DataAccess.handler;
using DataAccess.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Staff.Services;
using Modules.Employees.Controllers.Staff.ViewModels.EmployeeVM;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Modules.Employees.Controllers.Staff
{
    [Route("api/staff/employees")]
    [ApiController]
    public class StaffEmployeeController : ControllerBase
    {
        private readonly HttpContextAccessor _accessor;
        private readonly StaffEmployeeService _svc;
        private readonly JwtSecurityTokenHandler handler = new();


        public StaffEmployeeController(HttpContextAccessor accessor, IEmployeeRepository repo)
        {
            _accessor = accessor;
            _svc = new StaffEmployeeService(repo);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _svc.ReadAsync(GetEmail()));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(StaffEmployeeRequest request)
        {
            await _svc.UpdateAsync(request, GetEmail());
            return Ok("Success");
        }

        /**
         *  Hành vi lấy ra email
         */
        private string GetEmail()
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
