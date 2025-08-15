using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            // Repository örnekleri buraya atanır
            User = new UserRepository(_context);
            Role = new RoleRepository(_context);
            Department = new DepartmentRepository(_context);
            UserDetail = new UserDetailRepository(_context);
            Pozisyon = new PozisyonRepository(_context);
            LeaveRequest = new LeaveRequestRepository(_context);
            TaskItem = new TaskItemRepository(_context);
            AppNotification = new AppNotificationRepository(_context);
            Event = new EventRepository(_context);
            Payment = new PaymentRepository(_context);

        }

        public IUserRepository User { get; private set; }
        public IRoleRepository Role { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IUserDetailRepository UserDetail { get; private set; }
        public IPozisyonRepository Pozisyon { get; private set; }
        public ILeaveRequestRepository LeaveRequest { get; private set; }
        public ITaskItemRepository TaskItem { get; private set; }
        public IAppNotificationRepository AppNotification { get; private set; }
        public IEventRepository Event { get; private set; }
        public IPaymentRepository Payment { get; private set; }




        // Diğer repository'ler geldiğinde:
        // public ILeaveRequestRepository LeaveRequest { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
