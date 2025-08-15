namespace HRManagement.DTOs
{
    public class PozisyonDto
    {
        public int PozisyonId { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
