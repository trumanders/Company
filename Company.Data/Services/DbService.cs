using AutoMapper;
using Company.Data.Contexts;
using Microsoft.EntityFrameworkCore.Update;
using System.Linq.Expressions;
using System.Xml.Linq;


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


    // Method to get one record of a table (calls the method to get one entity)
    public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
       where TEntity : class, IEntity
       where TDto : class
    {
        var entity = await SingleAsync<TEntity>(expression);

        // Return TDto
        return _mapper.Map<TDto>(entity);
    }

    // Private overloaded SingleAsync method that only returns an entity
    // Check whether a matching record was found or not: Expression<Func<TEntity, bool>>
    private async Task<IEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
    {
        return await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
    }


    // Check if an item exists, AnyAsync
    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
    {
        // AnyAsync returns true if at least one record is found
        return await _db.Set<TEntity>().AnyAsync(expression);
    }

    // SaveChangesAsync
    public async Task<bool> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }

    // AddAsync
    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class, IEntity
        where TDto : class
    {
        // Convert dto to entity
        var entity = _mapper.Map<TEntity>(dto);

        // Add the entity by passing in the entity to AddAsync
        await _db.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    // UpdateAsync
    public bool Update<TEntity, TDto>(int id, TDto dto)
        where TEntity : class, IEntity
        where TDto : class
    {        
        // Convert dto to entity
        var entity = _mapper.Map<TEntity>(dto);
        if (id == entity.Id)
            return false;
        _db.Set<TEntity>().Update(entity);
        return true;
    }


    // DeleteAsync
    public async Task<bool> DeleteAsync<TEntity>(int id)
        where TEntity : class, IEntity
    {
        try
        {
            var entity = await SingleAsync<TEntity>(e => e.Id.Equals(id));
            if (entity == null)
                return false;
            _db.Remove(entity);
        }
        catch
        {
            throw;
        }
        return true;
    }


    // Delete method for reference entities (synchronous)
    public bool Delete<TReferenceEntity, TDto>(TDto dto)
        where TReferenceEntity : class , IReferenceEntity
        where TDto : class
    {
        try
        {
            
            var entity = _mapper.Map<TReferenceEntity>(dto);
            if (entity is null) return false;
            _db.Remove(entity);
        }
        catch
        {
            throw;
        }
        return true;
    }


    public async Task<TReferenceEntity> AddAsyncRef<TReferenceEntity, TDto>(TDto dto)
        where TReferenceEntity : class, IReferenceEntity
        where TDto : class
    {
        // Convert dto to entity
        var entity = _mapper.Map<TReferenceEntity>(dto);

        // Add the entity by passing in the entity to AddAsync
        await _db.Set<TReferenceEntity>().AddAsync(entity);
        return entity;
    }

    public string GetURIString<TEntity>(TEntity entity)
        where TEntity : class , IEntity
    {
        var node = typeof(TEntity).Name.ToLower();
        return $"/{node}s/{entity.Id}";
    }
}
