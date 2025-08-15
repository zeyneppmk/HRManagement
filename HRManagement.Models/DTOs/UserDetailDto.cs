namespace HRManagement.DTOs
{
    public class UserDetailDto
    {
        public int UserDetailId { get; set; }
        public int UserId { get; set; }

        public string? TCIdentity { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }

        public DateTime? StartDate { get; set; }
        public decimal? Salary { get; set; }

        public string? BankName { get; set; }
        public string? IBAN { get; set; }

        public string? PhotoPath { get; set; }
    }
}
