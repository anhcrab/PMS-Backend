using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Admin.ViewModels.DepartmentVM;

namespace Modules.Employees.Controllers.Admin
{
    [Route("api/admin/departments")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDepartmentController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(Array.Empty<AdminDepartmentResponse>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(new AdminDepartmentResponse { });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminDepartmentResponse request)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, AdminDepartmentResponse request)
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
