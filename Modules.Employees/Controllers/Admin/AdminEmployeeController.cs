using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Admin.ViewModels.EmployeeVM;

namespace Modules.Employees.Controllers.Admin
{
    [Route("api/admin/employees")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminEmployeeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(Array.Empty<AdminEmployeeResponse>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(new AdminEmployeeResponse { });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminEmployeeRequest request)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, AdminEmployeeRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok();
        }
    }
}
