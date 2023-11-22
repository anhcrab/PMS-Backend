using DataAccess.Entities;
using DataAccess.Repositories.EmployeeRepository;
using Modules.Employees.Controllers.Staff.ViewModels.EmployeeVM;

namespace Modules.Employees.Controllers.Staff.Services
{
    public class StaffEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public StaffEmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<StaffEmployeeResponse> ReadAsync(string email)
        {
            var self = await _repo.GetByEmailAsync(email);
            return new StaffEmployeeResponse
            {
                Id = self.Id,
                Hometown = self.Hometown,
                Position = self.Position,
                ConcurencyStamp = self.ConcurencyStamp,
                User = self.User,
                Supervisor = self.Supervisor,
                TeamMembers = self.TeamMembers,
                Department = self.Department,
            };
        }

        public async Task UpdateAsync(StaffEmployeeRequest request, string email)
        {
            var current = ReadAsync(email);
            await _repo.UpdateAsync(request.Id, new Employee
            {
                Id = request.Id,
                Hometown = request.Hometown,
            });
        }
    }
}
