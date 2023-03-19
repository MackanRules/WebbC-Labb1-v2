using Microsoft.AspNetCore.Mvc;
using VOD.Common.DTOs;
using VOD.Membership.Database.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimilarFilmsController : ControllerBase
    {

        public readonly IDbService _db;
        public SimilarFilmsController(IDbService db)
        {
            _db = db;

        }

        // GET: api/<SimilarFilmsController>
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                // Include?
                var similarFilm = await _db.GetReferenceAsync<SimilarFilm, SimilarFilmDTO>();
                return Results.Ok(similarFilm);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }

        /*
        // GET api/<SimilarFilmsController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                // Include?
                var similarFilm = await _db.SingleRefAsync<SimilarFilm, SimilarFilmDTO>(c => c.Id == id);
                return Results.Ok(similarFilm);
            }
            catch (Exception ex)
            {
                return Results.NotFound("Här gick det fel!");
            }

        }
        */

        // POST api/<SimilarFilmsController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] SimilarFilmCreateDTO dto)
        {
            try
            {
                var similarFilm = await _db.AddReferenceAsync<SimilarFilm, SimilarFilmCreateDTO>(dto);
                await _db.SaveChangesAsync();
                return Results.Ok();

                //return Results.Created(_db.GetURI<SimilarFilm>(similarFilm), similarFilm);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Här gick det fel!");
            }
        }

        // PUT api/<SimilarFilmsController>/5
        // no put

        // DELETE api/<SimilarFilmsController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(SimilarFilmDTO dto)
        {
            try
            {

                var success = _db.DeleteReference<SimilarFilm, SimilarFilmDTO>(dto);
                if (!success)
                {
                    return Results.NotFound("similarFilm not found");
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
