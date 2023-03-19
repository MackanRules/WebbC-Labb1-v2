using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOs;
using VOD.Membership.Database.Entities;
using VOD.Membership.Database.Services;

namespace VOD.Membership.Database.Extensions
{
    public static class VODContextExtensions
    {

        public static async Task SeedMembershipData(this IDbService service)
        {
            var description = " Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

            try
            {
                #region Directors
                await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
                {
                    Name = "Peter Karlsson"
                });
                await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
                {
                    Name = "Sven Svensson"
                });

                await service.SaveChangesAsync();
                #endregion

                #region Genre
                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Name = "Action"
                });
                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Name = "Sci-Fi"
                });
                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Name = "Horror"
                });
                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Name = "Thriller"
                });
                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Name = "History"
                });
                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Name = "Crime"
                });

                await service.SaveChangesAsync();
                #endregion

                #region Films
                await service.AddAsync<Film, FilmDTO>(new FilmDTO
                {
                    Title = "Spider man",
                    DirectorId = 1,
                    Released = new DateTime(2002, 5, 1, 8, 30, 52),
                    Description = description.Substring(10, 20),
                    FilmURL = "https://www.imdb.com/video/vi1376109081/?playlistId=tt0145487&ref_=tt_pr_ov_vi",
                    Free = true
                });

                await service.AddAsync<Film, FilmDTO>(new FilmDTO
                {
                    Title = "X-Files",
                    DirectorId = 1,
                    Released = new DateTime(1992, 5, 1, 8, 30, 52),
                    Description = description.Substring(10, 22),
                    FilmURL = "https://www.imdb.com/video/vi745453849/?playlistId=tt0106179&ref_=tt_ov_vi",
                    Free = true
                });

                await service.AddAsync<Film, FilmDTO>(new FilmDTO
                {
                    Title = "Avengers",
                    DirectorId = 2,
                    Released = new DateTime(2012, 5, 1, 8, 30, 52),
                    Description = description.Substring(5, 23),
                    FilmURL = "https://www.imdb.com/video/vi1891149081/?playlistId=tt0848228&ref_=tt_pr_ov_vi",
                    Free = true
                });

                await service.SaveChangesAsync();
                #endregion

                await service.AddReferenceAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO
                {
                    FilmId = 1,
                    GenreId = 1
                });
                await service.AddReferenceAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO
                {
                    FilmId = 1,
                    GenreId = 2
                });
                await service.AddReferenceAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO
                {
                    FilmId = 1,
                    GenreId = 3
                });
                await service.AddReferenceAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO
                {
                    FilmId = 2,
                    GenreId = 1
                });
                await service.AddReferenceAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO
                {
                    FilmId = 2,
                    GenreId = 2
                });
                await service.AddReferenceAsync<FilmGenre, FilmGenreDTO>(new FilmGenreDTO
                {
                    FilmId = 3,
                    GenreId = 2
                });
                await service.SaveChangesAsync();


                await service.AddReferenceAsync<SimilarFilm, SimilarFilmCreateDTO>(new SimilarFilmCreateDTO
                {
                    FilmId = 1,
                    SimilarFilmId = 2
                });
                await service.AddReferenceAsync<SimilarFilm, SimilarFilmCreateDTO>(new SimilarFilmCreateDTO
                {
                    FilmId = 2,
                    SimilarFilmId = 3
                });
                await service.AddReferenceAsync<SimilarFilm, SimilarFilmCreateDTO>(new SimilarFilmCreateDTO
                {
                    FilmId = 3,
                    SimilarFilmId = 1
                });
                await service.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
        }

    }
}
