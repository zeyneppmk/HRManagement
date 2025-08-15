using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Entitites
{
    public class Pozisyon
    {
        public int PozisyonId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; }             // Örn: IT Takım Lideri
        public string? Description { get; set; }      // Görev tanımı, opsiyonel
        public DateTime AssignedDate { get; set; } = DateTime.Now;

    }
}
