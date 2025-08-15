using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class AppNotificationRepository : Repository<AppNotification>, IAppNotificationRepository
    {
        private readonly AppDbContext _context;

        public AppNotificationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(AppNotification notification)
        {
            var objFromDb = _context.AppNotifications.FirstOrDefault(n => n.NotificationId == notification.NotificationId);

            if (objFromDb != null)
            {
                objFromDb.Title = notification.Title;
                objFromDb.Message = notification.Message;
                objFromDb.Type = notification.Type;
                objFromDb.Status = notification.Status;
                objFromDb.TaskItemId = notification.TaskItemId;
                objFromDb.LeaveRequestId = notification.LeaveRequestId;
                objFromDb.SenderId = notification.SenderId;
                objFromDb.ReceiverId = notification.ReceiverId;
            }
        }
    }
}
