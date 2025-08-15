namespace HRManagement.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }  // Güncelleme senaryoları için
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
