namespace Modules.Employees.Controllers.Manager.ViewModels.EmployeeVM
{
    public class ManagerEmployeeResponse
    {
        public string UserId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string MetaId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Sex { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Hometown { get; set; } = null!;
        public string SupervisorId { get; set; } = null!;
        public string SupervisorName { get; set; } = null!;
    }
}
