namespace HRManagement.DTOs
{
    public class AppNotificationDto
    {

        public int NotificationId { get; set; }

        // Listeleme için
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; } // Enum string
        public string? Status { get; set; } // Enum string
        public DateTime CreatedAt { get; set; }

        // Post/Put için
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int? TaskItemId { get; set; }
        public int? LeaveRequestId { get; set; }

        // Listeleme için (opsiyonel)
        public string? SenderName { get; set; }
        public string? ReceiverName { get; set; }
    }
    }



