using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
    public class SimilarFilmDTO
    {
        public int FilmId { get; set; }
        public int SimilarFilmId { get; set; }
    }
    public class SimilarFilmCreateDTO
    {
        public int FilmId { get; set; }
        public int SimilarFilmId { get; set; }

    }

}
