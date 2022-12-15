using System.Linq;
namespace Company.Data.Interfaces;

public interface IDbService
{
    // GetAsync

    // Create a List of Task of generic type
    // Specify that TEntity must be a class which implements IEntity (to have an Id)
    Task<List<TDto>> GetAsync<TEntity, TDto>()
        where TEntity : class, IEntity
        where TDto : class;

    // Method to return one record of a table
    Task<TDto> SingleAsync<TEntity, TDto>(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
        where TDto : class;
}

