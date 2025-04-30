using IndigoSoftTest.BusinessLogic.Entities;

namespace IndigoSoftTest.BusinessLogic.Repositories.Users;

public interface IUsersRepository : IRepository<User>
{
    Task<User?> GetByUserIdAsync(ulong userId);
}