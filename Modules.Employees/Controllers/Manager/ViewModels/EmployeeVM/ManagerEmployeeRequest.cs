namespace Modules.Employees.Controllers.Manager.ViewModels.EmployeeVM
{
    public class ManagerEmployeeRequest
    {
        public string Position { get; set; } = null!; // Chức vụ
        public string DepartmentId { get; set; } = null!; // Mã phòng ban
        public string UserId { get; set; } = null!; // Mã tài khoản
        public string SupervisorId { get; set; } = null!; // Mã người giám sát
        public string Hometown { get; set; } = null!; // Quê quán
        public string ConcurencyStamp { get; set; } = null!; // Mã với mục đích đảm bảo tính nhất quán của dữ liệu
    }
}
