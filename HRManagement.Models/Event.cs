using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Entitites
{
    public class Event
    {
        public int EventId { get; set; }
        public string? Title { get; set; }  // Ör: "Ramazan Bayramı", "Yılbaşı", "Toplantı"

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsFullDay { get; set; } = false;
        public bool IsRepeatable { get; set; } = false;

        public EventType Type { get; set; } = EventType.General;

    }

    public enum EventType
    {
        General = 0,     // Konferans, doğum günü vs.
        Holiday = 1      // Resmi tatil
    }

}
