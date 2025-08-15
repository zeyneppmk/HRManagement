using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Entitites
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }

        public string Title { get; set; }               // Görev başlığı
        public string Description { get; set; }         // Görev detayı

        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }          // Opsiyonel: son teslim tarihi

        public TaskStatus Status { get; set; } = TaskStatus.Planned;

        public int AssignedById { get; set; }           // Görevi veren kişi (Yönetici/Müdür)
        public User AssignedBy { get; set; }

        public int AssignedToId { get; set; }           // Görev atanan kişi (çalışan)
        public User AssignedTo { get; set; }
    }

    public enum TaskStatus
    {
        Planned,        // Bekliyor
        InProgress,     // Çalışılıyor
        Completed       // Tamamlandı
    }
}
