using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeJobTitleController : ControllerBase
    {

        private readonly IDbService _db;

        public EmployeeJobTitleController(IDbService db)
        {
            _db = db;
        }

        // POST api/<EmployeeJobTitleControllers>
        [HttpPost]
        public async Task<IResult> Post([FromBody] EmployeeJobTitleDTO dto)
        {
            var entity = _db.AddAsyncRef<EmployeeJobTitle, EmployeeJobTitleDTO>(dto);
            // DbService has now converted the dto to entity and added it to the database
            // The DbService gereic method returns the entity it created and added (entity)
            try
            {
                //Save changes to the database and return the node if successful
                if (await _db.SaveChangesAsync())
                {
                    // Create URI with with the name of the entity and Id and return
                    var node = typeof(EmployeeJobTitle).Name.ToLower();
                    return Results.Created($"/{node}s/{entity.Id}", entity);
                }
            }
            catch
            {
                return Results.BadRequest($"You could not add the {typeof(EmployeeJobTitle).Name} entity.");
            }
            return Results.BadRequest();
        }


        // DELETE api/<EmployeeJobTitleControllers> 
        [HttpDelete]
        public async Task<IResult> Delete(EmployeeJobTitleDTO dto)
        {
            try
            {
                if (!_db.Delete<EmployeeJobTitle, EmployeeJobTitleDTO>(dto))
                    return Results.NotFound();
                if (await _db.SaveChangesAsync())
                    return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Couldn't delete the {typeof(EmployeeJobTitle).Name} entity.\n{ex}.");

            }
            return Results.BadRequest($"Could not delete the id in {typeof(EmployeeJobTitle).Name}.");

        }
    }
}
