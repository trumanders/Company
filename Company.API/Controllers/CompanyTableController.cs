using Company.Data.Interfaces;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTableController : ControllerBase
    {
        private readonly IDbService _db;
        public CompanyTableController(IDbService db)
        {
            _db = db;
        }


        // GET: api/<CompanyTableControllers>
        [HttpGet]
        public async Task<IResult> Get()
        {
            // Wrap the result of the Get-method with the method Results.Ok which include status messages            
            return Results.Ok(await _db.GetAsync<CompanyTable, CompanyTableDTO>());
        }



        // GET api/<CompanyControllers>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var result = await _db.SingleAsync<CompanyTable, CompanyTableDTO>(e => e.Id.Equals(id));

            // Check result
            if (result == null)
                return Results.NotFound(result);
            else
                return Results.Ok(result);
        }



        // POST api/<CompanyControllers>
        [HttpPost]
        public async Task<IResult> Post([FromBody] CompanyTableDTO dto)
        {
            if (dto == null) return Results.BadRequest();
            var entity = await _db.AddAsync<CompanyTable, CompanyTableDTO>(dto);
            // DbService has now converted the dto to entity and added it to the database
            // The DbService gereic method returns the entity it created and added (entity)
            try
            {
                //Save changes to the database and return the node if successful
                if (await _db.SaveChangesAsync())               
                    // Create URI with with the name of the entity and Id and return                    
                    return Results.Created(_db.GetURIString<CompanyTable>(entity), entity);             
            }
            catch
            {
                return Results.BadRequest($"You could not add the {typeof(CompanyTable).Name} entity.");
            }
            return Results.BadRequest();
        }



        // PUT api/<CompanyControllers>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] CompanyTableDTO dto)
        {

            // Check if the entity and id exists in the database
            if (!await _db.AnyAsync<CompanyTable>(e => e.Id.Equals(id)))
                return Results.NotFound();

            try
            {
                _db.Update<CompanyTable, CompanyTableDTO>(id, dto);
                if (await _db.SaveChangesAsync())
                    return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest($"Could not update the {typeof(CompanyTable).Name} entity\n");
            }
            return Results.BadRequest();
        }




        // DELETE api/<CompanyControllers>
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (!await _db.DeleteAsync<CompanyTable>(id)) // Does not change in database until SaveChanges
                    return Results.NotFound();
                if (await _db.SaveChangesAsync())
                    return Results.NoContent();
            }
            catch(Exception ex)
            {
                return Results.BadRequest($"Couldn't delete the {typeof(CompanyTable).Name} entity.\n{ex}.");

            }
            return Results.BadRequest($"Could not delete the id in {typeof(CompanyTable).Name}.");
        }
    }
}
