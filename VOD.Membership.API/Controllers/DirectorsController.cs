using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {

        public readonly IDbService _db;
        public DirectorsController(IDbService db)
        {
            _db = db;
        }

        // GET: api/<DirectorsController>
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                // Include?
                var directors = await _db.GetAsync<Director, DirectorDTO>();
                return Results.Ok(directors);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }

        // GET api/<DirectorsController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                // Include?
                var director = await _db.SingleAsync<Director, DirectorDTO>(c => c.Id == id);
                return Results.Ok(director);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }

        // POST api/<DirectorsController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] DirectorCreateDTO dto)
        {
            try
            {
                var director = await _db.AddAsync<Director, DirectorCreateDTO>(dto);
                await _db.SaveChangesAsync();
                return Results.Created(_db.GetURI<Director>(director), director);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Här gick det fel!");
            }
        }

        // PUT api/<DirectorsController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] DirectorEditDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return Results.BadRequest("id missmatch");
                }
                var exists = await _db.AnyAsync<Director>(c => c.Id == id);
                if (!exists)
                {
                    return Results.BadRequest("instructor not found");
                }

               

                _db.Update<Director, DirectorEditDTO>(dto.Id, dto);
                var success = await _db.SaveChangesAsync();
                if (!success)
                {
                    return Results.BadRequest("failed to save");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Här gick det fel!");
            }
        }

        // DELETE api/<DirectorsController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {

                var success = await _db.DeleteAsync<Director>(id);
                if (!success)
                {
                    return Results.NotFound("Director not found");
                }

                success = await _db.SaveChangesAsync();
                if (!success)
                {
                    return Results.BadRequest("Could not save to db");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Här gick det fel!");
            }
        }
    }
}
