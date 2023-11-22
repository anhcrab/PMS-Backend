using DataAccess.Entities;
using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Identity;
using Modules.Employees.Controllers.Manager.ViewModels.EmployeeVM;

namespace Modules.Employees.Controllers.Manager.Services
{
    public class ManagerEmployeeService
    {
        private readonly IEmployeeRepository _erepo;
        private readonly IDepartmentRepository _drepo;
        private readonly UserManager<User> _usr;

        public ManagerEmployeeService(IEmployeeRepository erepo, IDepartmentRepository drepo, UserManager<User> usr)
        {
            _erepo = erepo;
            _drepo = drepo;
            _usr = usr;
        }

        public async Task<List<ManagerEmployeeResponse>> FindAll(string email)
        {
            var result = new List<ManagerEmployeeResponse>();
            var list = await _drepo.GetAllAsync();
            foreach (var item in list)
            {
                if(item.Members.Find(e => e.User.Email == email) != null)
                {
                    item.Members.ForEach(e =>
                    {
                        result.Add(new ManagerEmployeeResponse
                        {
                            UserId = e.UserId,
                            Username = e.User.UserName,
                            Email = e.User.Email,
                            PhoneNumber = e.User.PhoneNumber,
                            FirstName = e.User.Usermeta.FirstName,
                            LastName = e.User.Usermeta.LastName,
                            Dob = e.User.Usermeta.Dob,
                            Sex = e.User.Usermeta.Sex,
                            EmployeeId = e.Id,
                            Position = e.Position,
                            Hometown = e.Hometown,
                            SupervisorId = e.SupervisorId,
                            SupervisorName = e.Supervisor.User.Usermeta.FirstName + e.Supervisor.User.Usermeta.LastName
                        });
                    });
                }
            }
            return result;
        }

        public async Task<ManagerEmployeeResponse> FindById(string id, string email)
        {
            var e = await _erepo.GetAsync(id);
            if (!await IsManaged(e, email)) return null;

            return new ManagerEmployeeResponse
            {
                UserId = e.UserId,
                Username = e.User.UserName,
                Email = e.User.Email,
                PhoneNumber = e.User.PhoneNumber,
                FirstName = e.User.Usermeta.FirstName,
                LastName = e.User.Usermeta.LastName,
                Dob = e.User.Usermeta.Dob,
                Sex = e.User.Usermeta.Sex,
                EmployeeId = e.Id,
                Position = e.Position,
                Hometown = e.Hometown,
                SupervisorId = e.SupervisorId,
                SupervisorName = e.Supervisor.User.Usermeta.FirstName + e.Supervisor.User.Usermeta.LastName
            };
        }

        public async Task Update(string id, ManagerEmployeeRequest request, string email)
        {
            if (!await IsManager(email)) return;
            var employee = await _erepo.GetAsync(id);
            if (!await IsManaged(employee, email)) return;
            await _erepo.UpdateAsync(id, new Employee
            {
                Id = id,
                Position = request.Position,
                SupervisorId = request.SupervisorId,
                TeamMembers = request.TeamMembers
            });
        }

        private async Task<bool> IsManager(string email)
        {
            return true;
            var manager = await _erepo.GetByEmailAsync(email);
            return await _usr.IsInRoleAsync(manager.User, "Manager");
        }

        private async Task<bool> IsManaged(Employee employee, string email)
        {
            if (!await IsManager(email)) return false;
            var manager = await _erepo.GetByEmailAsync(email);
            return manager.Department.Members.Contains(employee);
        }
    }
}
