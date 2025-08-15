using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Entitites
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public bool IsActive { get; set; } = true;

        public UserDetail UserDetail { get; set; } // One-to-one

        public ICollection<Pozisyon> Pozisyons { get; set; } // 1 -> * ilişki
    }

}
