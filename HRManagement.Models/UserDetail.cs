using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Entitites
{
    public class UserDetail
    {
        public int UserDetailId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string TCIdentity { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }

        public DateTime? StartDate { get; set; }
        public decimal? Salary { get; set; }

        public string BankName { get; set; }
        public string IBAN { get; set; }

        public string PhotoPath { get; set; }
    }

}
