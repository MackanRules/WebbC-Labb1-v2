﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
    public class FilmGenreDTO
    {
        public int FilmId { get; set; }
        public int GenreId { get; set; }
    }

    public class FilmGenreDeleteDTO
    {
        public int FilmId { get; set; }
        public int GenreId { get; set; }
    }

    public class FilmGenreCreateDTO : FilmGenreDeleteDTO
    {
    }

}
