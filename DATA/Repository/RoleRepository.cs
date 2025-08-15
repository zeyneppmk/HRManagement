using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
