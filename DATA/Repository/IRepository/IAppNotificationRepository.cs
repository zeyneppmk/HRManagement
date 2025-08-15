using HRManagement.Entitites;

namespace HRManagement.Data.Repository.IRepository
{
    public interface IAppNotificationRepository : IRepository<AppNotification>
    {
        void Update(AppNotification notification);
    }
}
