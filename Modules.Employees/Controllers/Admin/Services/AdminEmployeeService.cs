using DataAccess.Entities;
using DataAccess.Repositories.EmployeeRepository;
using Modules.Employees.Controllers.Admin.ViewModels.EmployeeVM;

namespace Modules.Employees.Controllers.Admin.Services
{
    public class AdminEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public AdminEmployeeService(IEmployeeRepository repo) 
        {
            _repo = repo;
        }
        public async Task<List<AdminEmployeeResponse>> getAll()
        {
            var list = await _repo.GetAllAsync();
            // TODO: Mapping
            var result = new List<AdminEmployeeResponse>();
            foreach (var item in list) 
            {
                result.Add(new AdminEmployeeResponse
                {
                    UserId = item.UserId,
                    Username = item.User.UserName,
                    Email = item.User.Email,
                    PhoneNumber = item.User.PhoneNumber,
                    MetaId = item.User.MetaId,
                    FirstName = item.User.Usermeta.FirstName,
                    LastName = item.User.Usermeta.LastName,
                    Dob = item.User.Usermeta.Dob,
                    Sex = item.User.Usermeta.Sex,
                    EmployeeId = item.Id,
                    Position = item.Position,
                    Hometown = item.Hometown,
                    SupervisorId = item.SupervisorId,
                    SupervisorName = item.Supervisor.User.Usermeta.FirstName + item.User.Usermeta.LastName,
                    DepartmentId = item.DepartmentId,
                    DepartmentName = item.Department.Name
                });
            }

            return result;
        }

        public async Task<AdminEmployeeResponse> GetById(string id)
        {
            var item = await _repo.GetAsync(id);
            return new AdminEmployeeResponse
            {
                UserId = item.UserId,
                Username = item.User.UserName,
                Email = item.User.Email,
                PhoneNumber = item.User.PhoneNumber,
                MetaId = item.User.MetaId,
                FirstName = item.User.Usermeta.FirstName,
                LastName = item.User.Usermeta.LastName,
                Dob = item.User.Usermeta.Dob,
                Sex = item.User.Usermeta.Sex,
                EmployeeId = item.Id,
                Position = item.Position,
                Hometown = item.Hometown,
                SupervisorId = item.SupervisorId,
                SupervisorName = item.Supervisor.User.Usermeta.FirstName + item.User.Usermeta.LastName,
                DepartmentId = item.DepartmentId,
                DepartmentName = item.Department.Name
            };
        }

        public async Task<string> AddNew(string id)
        {
            // Kiểm tra nhân viên đã có tài khoản hay chưa nếu có thì
            // cho phép đăng ký tài khoản đó thành nhân viên nếu không cút.

            var notFound = _repo.GetAsync(id) == null;
            if (notFound) return "";
            // Mapping
            var employee = new Employee
            {
                // Đăng ký nhân viên mới với role là Staff
                Position = "Staff",
                DepartmentId = "",
                UserId = id,
                SupervisorId = "",
                Hometown = "",
            };
            return await _repo.AddAsync(employee);
        }

        public async Task Remove(string id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task UpdateOne(string id, AdminEmployeeRequest request)
        {
            
            // TODO: Mapping
            var employee = new Employee 
            {
                Position = request.Position,
                DepartmentId = request.DepartmentId,
                UserId = request.UserId,
                SupervisorId = request.SupervisorId,
                Hometown = request.Hometown,
            };
            await _repo.UpdateAsync(id, employee);
        }
    }
}
