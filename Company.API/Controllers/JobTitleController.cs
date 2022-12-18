using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitleController : ControllerBase
    {
        private readonly IDbService _db;

        public JobTitleController(IDbService db)
        {
            _db = db;
        }


        // GET: api/<JobTitleControllers>
        [HttpGet]
        public async Task<IResult> Get()
        {
            // Wrap the result of the Get-method with the method Results.Ok which include status messages            
            return Results.Ok(await _db.GetAsync<JobTitle, JobTitleDTO>());
        }



        // GET api/<JobTitleControllers>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var result = await _db.SingleAsync<JobTitle, JobTitleDTO>(e => e.Id.Equals(id));

            // Check result
            if (result == null)
                return Results.NotFound(result);
            else
                return Results.Ok(result);
        }



        // POST api/<JobTitleControllers>
        [HttpPost]
        public async Task<IResult> Post([FromBody] JobTitleDTO dto)
        {
            var entity = await _db.AddAsync<JobTitle, JobTitleDTO>(dto);
            // DbService has now converted the dto to entity and added it to the database
            // The DbService gereic method returns the entity it created and added (entity)
            try
            {
                //Save changes to the database and return the node if successful
                if (await _db.SaveChangesAsync())
                {
                    // Create URI with with the name of the entity and Id and return
                    var node = typeof(JobTitle).Name.ToLower();
                    return Results.Created($"/{node}s/{entity.Id}", entity);
                }
            }
            catch
            {
                return Results.BadRequest($"You could not add the {typeof(JobTitle).Name} entity.");
            }
            return Results.BadRequest();
        }



        // PUT api/<JobTitleControllers>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] JobTitleDTO dto)
        {

            // Check if the entity and id exists in the database
            if (!await _db.AnyAsync<JobTitle>(e => e.Id.Equals(id)))
                return Results.NotFound();

            try
            {
                _db.Update<JobTitle, JobTitleDTO>(id, dto);
                if (await _db.SaveChangesAsync())
                    return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest($"Could not update the {typeof(JobTitle).Name} entity\n");
            }
            return Results.BadRequest();
        }




        // DELETE api/<JobTitleControllers>
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (!await _db.DeleteAsync<JobTitle>(id))
                    return Results.NotFound();
                if (await _db.SaveChangesAsync())
                    return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Couldn't delete the {typeof(JobTitle).Name} entity.\n{ex}.");

            }
            return Results.BadRequest($"Could not delete the id in {typeof(JobTitle).Name}.");
        }
    }
}
