namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDbService _db;

        public DepartmentController(IDbService db)
        {
            _db = db;
        }


        // GET: api/<DepartmentControllers>
        [HttpGet]
        public async Task<IResult> Get()
        {
            // Wrap the result of the Get-method with the method Results.Ok which include status messages            
            return Results.Ok(await _db.GetAsync<Department, DepartmentDTO>());
        }



        // GET api/<DepartmentControllers>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var result = await _db.SingleAsync<Department, DepartmentDTO>(e => e.Id.Equals(id));

            // Check result
            if (result == null)
                return Results.NotFound(result);
            else
                return Results.Ok(result);
        }



        // POST api/<DepartmentControllers>
        [HttpPost]
        public async Task<IResult> Post([FromBody] DepartmentDTO dto)
        {
            var entity = await _db.AddAsync<Department, DepartmentDTO>(dto);
            // DbService has now converted the dto to entity and added it to the database
            // The DbService gereic method returns the entity it created and added (entity)
            try
            {
                //Save changes to the database and return the node if successful
                if (await _db.SaveChangesAsync())
                {
                    // Create URI with with the name of the entity and Id and return
                    var node = typeof(Department).Name.ToLower();
                    return Results.Created($"/{node}s/{entity.Id}", entity);
                }
            }
            catch
            {
                return Results.BadRequest($"You could not add the {typeof(Department).Name} entity.");
            }
            return Results.BadRequest();
        }



        // PUT api/<DepartmentControllers>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] DepartmentDTO dto)
        {

            // Check if the entity and id exists in the database
            if (!await _db.AnyAsync<Department>(e => e.Id.Equals(id)))
                return Results.NotFound();

            try
            {
                _db.Update<Department, DepartmentDTO>(id, dto);
                if (await _db.SaveChangesAsync())
                    return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest($"Could not update the {typeof(Department).Name} entity\n");
            }
            return Results.BadRequest();
        }




        // DELETE api/<DepartmentControllers>
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (!await _db.DeleteAsync<Department>(id))
                    return Results.NotFound();
                if (await _db.SaveChangesAsync())
                    return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Couldn't delete the {typeof(Department).Name} entity.\n{ex}.");

            }
            return Results.BadRequest($"Could not delete the id in {typeof(Department).Name}.");
        }
    }
}