using DataAccess.Entities;

namespace Modules.Employees.Controllers.Manager.ViewModels.EmployeeVM
{
    public class ManagerEmployeeRequest
    {
        public string Position { get; set; } = null!; // Vị trí công việc
        public string UserId { get; set; } = null!; // Mã tài khoản
        public string SupervisorId { get; set; } = null!; // Mã người giám sát
        public List<Employee> TeamMembers { get; set; } = new();
        public string ConcurencyStamp { get; set; } = null!; // Mã với mục đích đảm bảo tính nhất quán của dữ liệu
    }
}
