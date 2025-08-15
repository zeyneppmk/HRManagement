using HRManagement.Entitites;
namespace HRManagement.DTOs
{
    public class LeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }

        public string? ManagerNote { get; set; }

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending; // Default: Beklemede

        public DateTime CreatedAt { get; set; }
        // Görsellik için eklendi:
        public string? UserName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
