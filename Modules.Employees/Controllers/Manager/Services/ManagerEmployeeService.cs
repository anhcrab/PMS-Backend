using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.EmployeeRepository;
using Modules.Employees.Controllers.Manager.ViewModels.EmployeeVM;

namespace Modules.Employees.Controllers.Manager.Services
{
    public class ManagerEmployeeService
    {
        private readonly IEmployeeRepository _erepo;
        private readonly IDepartmentRepository _drepo;

        public ManagerEmployeeService(IEmployeeRepository erepo, IDepartmentRepository drepo)
        {
            _erepo = erepo;
            _drepo = drepo;
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
                            MetaId = e.User.MetaId,
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
            var result = new List<ManagerEmployeeResponse>();
            var list = await _drepo.GetAllAsync();
            foreach ( var item in list)
            {
                if (item.Members.Find(e => e.User.Email == email) != null)
                {
                    var e = item.Members.Find(e => e.UserId ==  id);
                    return new ManagerEmployeeResponse
                    {
                        UserId = e.UserId,
                        Username = e.User.UserName,
                        Email = e.User.Email,
                        PhoneNumber = e.User.PhoneNumber,
                        MetaId = e.User.MetaId,
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
            }
            return null!;
        }

        public async Task<string> Add(ManagerEmployeeRequest request)
        {
            // TODO
            return "";
        }

        public async Task Update(string id, ManagerEmployeeRequest request)
        {
            // TODO
        }

        public async Task Remove(string id)
        {
            // TODO
        }
    }
}
