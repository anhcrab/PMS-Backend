using DataAccess.Entities;
using DataAccess.handler;
using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            IHttpContextAccessor accessor,
            UserManager<User> usr
        )
        {
            _svc = new ManagerEmployeeService(repo1, repo2, usr);
            _accessor = accessor;
        }

        /**
         * Hành vi đọc toàn bộ nhân viên trong phòng ban của Manager
         */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var email = GetManagerEmail();
            return Ok(await _svc.FindAll(email!));
        }

        /**
         * Hành vi đọc thông tin của 1 nhân viên cụ thể trong phòng ban của Manager
         */
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var email = GetManagerEmail();
            return Ok(await _svc.FindById(id, email!));
        }

        /**
         * Hành vi thay đổi chức vụ và người giám sát
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, ManagerEmployeeRequest request)
        {
            var email = GetManagerEmail();
            await _svc.Update(id, request, email);
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
