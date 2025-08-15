using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Entitites
{
    public class AppNotification
    {

        [Key]
        public int NotificationId { get; set; }

        public int SenderId { get; set; }       // Gönderen kişi (örn: Departman Müdürü)
        public User Sender { get; set; }

        public int ReceiverId { get; set; }     // Alıcı kişi
        public User Receiver { get; set; }

        public string Title { get; set; }       // Başlık (örn: İzin Talebi, Görev Ataması)
        public string Message { get; set; }     // Detaylı açıklama veya mesaj

        public NotificationType Type { get; set; } // İzin, Task, Bilgilendirme, Uyarı

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public NotificationStatus? Status { get; set; } // Task ve LeaveRequest gibi tiplerde durumu olur

        // Task bağlantısı (opsiyonel)
        public int? TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }

        // LeaveRequest bağlantısı (opsiyonel)
        public int? LeaveRequestId { get; set; }
        public LeaveRequest? LeaveRequest { get; set; }
    }

    public enum NotificationType
    {
        Task,
        Leave,
        Info,       // Bilgilendirme
        Warning     // Uyarı
    }

    public enum NotificationStatus
    {
        Pending,     // Bekliyor
        Approved,    // Onaylandı
        Rejected     // Reddedildi
    }
}
