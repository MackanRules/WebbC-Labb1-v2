using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        public readonly IDbService _db;
        public GenresController(IDbService db)
        {
            _db = db;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                // Include?
                var genres = await _db.GetAsync<Genre, GenreDTO>();
                return Results.Ok(genres);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                // Include?
                var genre = await _db.SingleAsync<Genre, GenreDTO>(c => c.Id == id);
                return Results.Ok(genre);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] GenreCreateDTO dto)
        {
            try
            {
                var genre = await _db.AddAsync<Genre, GenreCreateDTO>(dto);
                await _db.SaveChangesAsync();
                return Results.Created(_db.GetURI<Genre>(genre), genre);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Här gick det fel!");
            }
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] GenreEditDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return Results.BadRequest("id missmatch");
                }
                var exists = await _db.AnyAsync<Genre>(c => c.Id == id);
                if (!exists)
                {
                    return Results.BadRequest("Genre not found");
                }



                _db.Update<Genre, GenreEditDTO>(dto.Id, dto);
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

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {

                var success = await _db.DeleteAsync<Genre>(id);
                if (!success)
                {
                    return Results.NotFound("Genre not found");
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
