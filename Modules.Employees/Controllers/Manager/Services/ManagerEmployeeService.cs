using DataAccess.Repositories.EmployeeRepository;
using Modules.Employees.Controllers.Manager.ViewModels.EmployeeVM;

namespace Modules.Employees.Controllers.Manager.Services
{
    public class ManagerEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public ManagerEmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ManagerEmployeeResponse>> FindAll()
        {
            var result = new List<ManagerEmployeeResponse>();
            var list = await _repo.GetAllAsync();
            foreach (var item in list)
            {
                result.Add(new ManagerEmployeeResponse
                {
                    // TODO
                });
            }
            return result;
        }

        public async Task<ManagerEmployeeResponse> FindById(string id)
        {
            var item = await _repo.GetAsync(id);
            return new ManagerEmployeeResponse
            {
                // TODO
            };
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
