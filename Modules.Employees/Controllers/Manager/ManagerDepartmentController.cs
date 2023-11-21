using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Modules.Employees.Controllers.Manager
{
    [Route("api/manager/departments")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerDepartmentController : ControllerBase
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll() 
        //{
        //    return Ok();
        //}
        //[HttpGet]
        //public async Task<IActionResult> Get(string id) 
        //{
        //    return Ok();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Add() 
        //{ 
        //    return Ok();

        //}

        //[HttpPut]
        //public async Task<IActionResult> Update() 
        //{
        //    return Ok();

        //}
        //[HttpDelete]
        //public async Task<IActionResult> Delete() 
        //{
        //    return Ok();

        //}
    }
}
