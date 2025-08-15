using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
