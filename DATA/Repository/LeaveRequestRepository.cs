using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class LeaveRequestRepository : Repository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly AppDbContext _context;

        public LeaveRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
