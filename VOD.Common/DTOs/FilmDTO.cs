using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
    public class FilmDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DirectorId { get; set; }
        public DateTime Released { get; set; }
        public bool Free { get; set; }
        public string FilmURL { get; set; }

        // För att hämta director namn direkt, genre, similar film
        public virtual DirectorDTO Director { get; set; }
        public virtual ICollection<GenreDTO> Genres { get; set; }
        public virtual ICollection<SimilarFilmDTO> SimilarFilms { get; set; }
    }

    public class FilmCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DirectorId { get; set; }
        public DateTime Released { get; set; }
        public bool Free { get; set; }
        public string FilmURL { get; set; }

        public FilmGenreDTO? FilmGenre { get; set; }
    }

    public class FilmEditDTO : FilmCreateDTO
    {
        public int? Id { get; set; }
        public virtual List<GenreDTO> Genres { get; set; } = new();
    }

}
