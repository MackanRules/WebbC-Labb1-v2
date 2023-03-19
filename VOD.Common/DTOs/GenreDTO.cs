using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GenreCreateDTO
    {
        public string? Name { get; set; }
    }

    public class GenreEditDTO : GenreCreateDTO
    {
        public int Id { get; set; }
    }

}
