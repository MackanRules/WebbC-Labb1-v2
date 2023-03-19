using System.ComponentModel.DataAnnotations;

namespace VOD.Common.DTOs
{
    public class DirectorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //För att hämta alla filmer från en instruktör
        public virtual ICollection<FilmDTO> Films { get; set; }
    }

    public class DirectorCreateDTO
    {
        public string? Name { get; set; }

    }

    public class DirectorEditDTO : DirectorCreateDTO
    {
        public int Id { get; set; }
    }

}