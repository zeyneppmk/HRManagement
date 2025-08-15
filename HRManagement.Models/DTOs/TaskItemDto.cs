namespace HRManagement.DTOs
{
    public class TaskItemDto
    {
        public int TaskItemId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTime AssignedDate { get; set; }
        public DateTime? DueDate { get; set; }

        public int AssignedById { get; set; }
        public int AssignedToId { get; set; }

        public int Status { get; set; } // Enum için int kullanıldı
        public string? AssignedToName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
