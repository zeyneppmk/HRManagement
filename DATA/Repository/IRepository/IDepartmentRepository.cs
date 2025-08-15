using HRManagement.Entitites;

namespace HRManagement.Data.Repository.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Department Get(int id);
    }
}
