using DataAccess.Repositories.DepartmentRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Admin.Services;
using Modules.Employees.Controllers.Admin.ViewModels.DepartmentVM;

namespace Modules.Employees.Controllers.Admin
{
    [Route("api/admin/departments")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDepartmentController : ControllerBase
    {
        private readonly AdminDepartmentService _svc;

        public AdminDepartmentController(IDepartmentRepository repo) 
        {
            _svc = new AdminDepartmentService(repo);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _svc.FindAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _svc.FindById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminDepartmentRequest request)
        {
            return Ok(await _svc.Add(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, AdminDepartmentRequest request)
        {
            await _svc.Update(id, request);
            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _svc.Remove(id);
            return Ok("Success");
        }
    }
}
