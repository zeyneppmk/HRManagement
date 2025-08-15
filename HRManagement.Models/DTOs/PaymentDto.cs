using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentPeriod { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}

