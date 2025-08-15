using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class UserDetailRepository : Repository<UserDetail>, IUserDetailRepository
    {
        private readonly AppDbContext _context;

        public UserDetailRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
