using IndigoSoftTest.BusinessLogic.Entities;

namespace IndigoSoftTest.BusinessLogic.Repositories;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T item);
}