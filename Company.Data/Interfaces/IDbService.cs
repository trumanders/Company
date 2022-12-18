using System.Linq.Expressions;
namespace Company.Data.Interfaces;

public interface IDbService
{
    // GetAsync

    // Create a List of Task of generic type
    // Specify that TEntity must be a class which implements IEntity (to have an Id)
    public Task<List<TDto>> GetAsync<TEntity, TDto>()
        where TEntity : class, IEntity
        where TDto : class;

    // Method to return one record of a table
    public Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
        where TDto : class;


    // Check if an item exists, AnyAsync
    public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity;


    // SaveChangesAsync
    public Task<bool> SaveChangesAsync();


    // AddAsync
    public Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class, IEntity
        where TDto : class;


    // AddAsync reference
    public Task<TReferenceEntity> AddAsyncRef<TReferenceEntity, TDto>(TDto dto)
     where TReferenceEntity : class, IReferenceEntity
     where TDto : class;


    // UpdateAsync
    public bool Update<TEntity, TDto>(int id, TDto dto)
        where TEntity: class, IEntity
        where TDto : class;


    // DeleteAsync
    public Task<bool> DeleteAsync<TEntity>(int id)
        where TEntity : class, IEntity;


    // Delete method for reference entities (synchronous)
    public bool Delete<TReferenceEntity, TDto>(TDto dto)
        where TReferenceEntity : class , IReferenceEntity
        where TDto : class;

    public string GetURIString<TEntity>(TEntity entity)
        where TEntity : class, IEntity;

 

}

