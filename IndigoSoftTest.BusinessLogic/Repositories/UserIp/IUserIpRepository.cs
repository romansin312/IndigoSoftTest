namespace IndigoSoftTest.BusinessLogic.Repositories.UserIp;

public interface IUserIpRepository : IRepository<Entities.UserIp>
{
    public Task<Entities.UserIp?> GetByUserAndIp(ulong userId, Guid ipId);
}