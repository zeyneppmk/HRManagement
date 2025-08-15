using System;

namespace HRManagement.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IDepartmentRepository Department { get; }
        IUserDetailRepository UserDetail { get; }
        IPozisyonRepository Pozisyon { get; }
        ILeaveRequestRepository LeaveRequest { get; }
        ITaskItemRepository TaskItem { get; }
        IAppNotificationRepository AppNotification { get; }
        IEventRepository Event { get; }
        IPaymentRepository Payment { get; }
        // Diğer tablolar geldikçe buraya eklenecek:
        // ILeaveRequestRepository LeaveRequest { get; }
        // IDepartmentRepository Department { get; }s

        void Save();
    }
}
