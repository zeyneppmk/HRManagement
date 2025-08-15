using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Entitites
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int UserId { get; set; } // Ödeme alan kullanıcı
        public User User { get; set; }

        public decimal Amount { get; set; } // Ödenen maaş tutarı
        public DateTime PaymentDate { get; set; } // Ödemenin yapıldığı tarih

        public string PaymentPeriod { get; set; } // Örn: "07/2025"
        public string Description { get; set; } // Açıklama (opsiyonel, örn: "Temmuz 2025 maaşı")
    }

}
