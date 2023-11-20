using DataAccess.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Employees.Controllers.Manager.Services;
using Modules.Employees.Controllers.Manager.ViewModels.EmployeeVM;

namespace Modules.Employees.Controllers.Manager
{
    [Route("api/manager/employees")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerEmployeeController : ControllerBase
    {
        private readonly ManagerEmployeeService _svc;

        public ManagerEmployeeController(IEmployeeRepository repo)
        {
            _svc = new ManagerEmployeeService(repo);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _svc.FindAll());
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id) 
        {
            return Ok(await _svc.FindById(id));
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
        public async Task<IActionResult > Delete(string id)
        {
            await _svc.Remove(id);
            return Ok("Success");
        }
    }
}
