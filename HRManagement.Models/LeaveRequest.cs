using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Entitites
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; } 

        public int UserId { get; set; }           // İzni talep eden kullanıcı
        public User User { get; set; }

        public DateTime StartDate { get; set; }   // İznin başlangıç tarihi
        public DateTime EndDate { get; set; }     // Bitiş tarihi

        public string Reason { get; set; }        // Kullanıcının izin açıklaması

        public string? ManagerNote { get; set; }  // Müdür/Yönetici açıklaması (opsiyonel)

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending; // Default: Beklemede

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum LeaveStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }
}
