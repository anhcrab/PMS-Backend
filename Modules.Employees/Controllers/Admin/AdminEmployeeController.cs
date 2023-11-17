using DataAccess.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Admin.Services;
using Modules.Employees.Controllers.Admin.ViewModels.EmployeeVM;

namespace Modules.Employees.Controllers.Admin
{
    [Route("api/admin/employees")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminEmployeeController : ControllerBase
    {
        private readonly AdminEmployeeService _svc;

        public AdminEmployeeController(IEmployeeRepository repo) 
        {
            _svc = new AdminEmployeeService(repo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _svc.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _svc.GetById(id));
        }

        [HttpGet("add/{id}")]
        public async Task<IActionResult> Add(string id)
        {
            return Ok(await _svc.AddNew(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, AdminEmployeeRequest request)
        {
            await _svc.UpdateOne(id, request);
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
