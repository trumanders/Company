using Company.Common.DTOs;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTableController : ControllerBase
    {
        private readonly IDbService _db;
        public CompanyTableController(IDbService db)
        {
            _db= db;
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CompanyControllers>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompanyControllers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
