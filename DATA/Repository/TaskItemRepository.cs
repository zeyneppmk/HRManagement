using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        private readonly AppDbContext _context;

        public TaskItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
