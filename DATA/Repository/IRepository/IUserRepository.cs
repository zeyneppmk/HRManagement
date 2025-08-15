using HRManagement.Entitites;

namespace HRManagement.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        // Gerekirse kullanıcıya özel sorgular buraya eklenebilir
        User GetUserByEmail(string email);

        User Get(int id);
    }
}
