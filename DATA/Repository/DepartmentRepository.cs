using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Department Get(int id)
        {
            return _context.Departments.Find(id)!;
        }
    }
}
