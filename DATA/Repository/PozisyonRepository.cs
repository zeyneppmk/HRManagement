using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class PozisyonRepository : Repository<Pozisyon>, IPozisyonRepository
    {
        private readonly AppDbContext _context;

        public PozisyonRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
