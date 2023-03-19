using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmGenresController : ControllerBase
    {

        public readonly IDbService _db;
        public FilmGenresController(IDbService db)
        {
            _db = db;
        }

        // GET: api/<FilmGenresController>
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                // Include?
                var filmgenres = await _db.GetReferenceAsync<FilmGenre, FilmGenreDTO>();
                return Results.Ok(filmgenres);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }

        /*
        // GET api/<FilmGenresController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                // Include?
                var director = await _db.SingleRefAsync<FilmGenre, FilmGenreDTO>(c => c.Id == id);
                return Results.Ok(director);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }
        */

        // POST api/<FilmGenresController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmGenreCreateDTO dto)
        {
            try
            {
                var filmgenre = await _db.AddReferenceAsync<FilmGenre, FilmGenreCreateDTO>(dto);
                await _db.SaveChangesAsync();
                return Results.Ok();
           
                //return Results.Created(_db.GetURI<FilmGenre>(filmgenre), filmgenre);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Här gick det fel!");
            }
        }

        // PUT api/<FilmGenresController>/5
        // no put

        // DELETE api/<FilmGenresController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(FilmGenreDTO dto)
        {
            try
            {

                var success = _db.DeleteReference<FilmGenre, FilmGenreDTO>(dto);
                if (!success)
                {
                    return Results.NotFound("filmGenre not found");
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
