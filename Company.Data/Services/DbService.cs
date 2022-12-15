using AutoMapper;
using Company.Data.Contexts;
using System.Linq.Expressions;


namespace Company.Data.Services;

/// <summary>
/// Contains generic CRUD-operations on tables in any entity
/// that implements IEntity (or IReferenceEntity for EmployeeJobTitle)
/// </summary>
public class DbService : IDbService
{
    // Create an object of CompanyContext
    private readonly CompanyContext _db;

    // Create object of the AutoMapper's IMapper interface
    // to convert between DTO and Entity
    private readonly IMapper _mapper;

    // CONSTRUCTOR - Inject CompanyContext to give access to the database (Db-sets)
    // Inject the AutoMapper's interface IMapper to convert between entity to DTO
    public DbService(CompanyContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }


    // GetAsync
    public async Task<List<TDto>> GetAsync<TEntity, TDto>()
        where TEntity : class, IEntity
        where TDto : class
    {
        // Create a List of the type (entity) that is passed in.
        var entities = await _db.Set<TEntity>().ToListAsync();

        // Return a List of IMapper with DTOs
        return _mapper.Map<List<TDto>>(entities);
    }


    /* SINGLE ASYNC (get one record of a table) */

    // Method to get one record of a table (calls the method to get one entity)
    public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
       where TEntity : class, IEntity
       where TDto : class
    {        
        var entity = await SingleAsync<TEntity>(expression);
        return _mapper.Map<TDto>(entity);
    }

    // Private method that returns an entity
    // Check wheter a matching record was found or not: Expression<Func<TEntity, bool>>
    private async Task<IEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
    {
        return await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
    }
}
