using IndigoSoftTest.BusinessLogic.Entities;

namespace IndigoSoftTest.BusinessLogic.Repositories.Users;

public class RepositoryBase<T>(IndigoSoftTestDbContext dbContext) : IRepository<T> where T : class
{
    public async Task CreateAsync(T item)
    {
        await dbContext.AddAsync(item);
    }
}