using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {

        public readonly IDbService _db;
        public FilmsController(IDbService db)
        {
            _db = db;
        } 

        // GET: api/<FilmsController>
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<Film>(); // orsakar loop
                // mer includes?
                var films = await _db.GetAsync<Film, FilmDTO>();
                return Results.Ok(films);
            }
            catch (Exception ex) 
            {
                return Results.NotFound("Här gick det fel!");
            }
            
        }

        // GET api/<FilmsController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                // await _db.Include<Film>();
                // await _db.IncludeReferenceAsync<FilmGenre>();
                var film = await _db.SingleAsync<Film, FilmDTO>(c => c.Id == id);
                return Results.Ok(film);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }

        // POST api/<FilmsController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmCreateDTO dto)
        {
            try
            {
                var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);
                await _db.SaveChangesAsync();
                return Results.Created(_db.GetURI<Film>(film), film);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Här gick det fel!");
            }
        }

        // PUT api/<FilmsController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] FilmEditDTO dto)
        {
            try
            {
                if(id != dto.Id)
                {
                    return Results.BadRequest("id missmatch");
                }
                var exists = await _db.AnyAsync<Director>(c => c.Id == dto.DirectorId);
                if (!exists)
                {
                    return Results.BadRequest("instructor not found");
                }

                exists = await _db.AnyAsync<Film>(c => c.Id == id);
                if (!exists)
                {
                    return Results.BadRequest("film not found");
                }

                _db.Update<Film, FilmEditDTO>(id, dto);
                await _db.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Här gick det fel!");
            }
        }

        // DELETE api/<FilmsController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                
                var exists = await _db.AnyAsync<Film>(c => c.Id == id);
                if (!exists)
                {
                    return Results.BadRequest("film not found");
                }

                var success = await _db.DeleteAsync<Film>(id);
                if (!success)
                {
                    return Results.NotFound("film not found");
                }

                var result = await _db.SaveChangesAsync();
                if(!result)
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
