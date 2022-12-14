using Company.Common.DTOs;

namespace Company.Data.Interfaces;

public interface IDbService
{
    // GetAsync

    // Create a List of Task of generic type
    // Specify that TEntity must be a class which implements IEntity (to have an Id)
    Task<List<TDto>> GetAsync<TEntity, TDto>()
        where TEntity : class, IEntity
        where TDto : class;
}
