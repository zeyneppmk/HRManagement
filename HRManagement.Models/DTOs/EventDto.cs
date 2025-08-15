using HRManagement.Entitites;

namespace HRManagement.DTOs
{
    public class EventDto
    {
            
        public int EventId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsFullDay { get; set; }
        public bool IsRepeatable { get; set; }
        public EventType Type { get; set; } = EventType.General; // Enum'u string olarak döndürüyoruz
    }
}
