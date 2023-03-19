using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Membership.Database.Entities
{
    public class Film : IEntity
    {
        public int Id { get ; set; }
        [MaxLength(50), Required]
        public string Title { get; set; }
        [MaxLength(200), Required]
        public string Description { get; set; }
        public int DirectorId { get; set; }
        public DateTime Released { get; set; }
        public bool Free { get; set; }
        [MaxLength(1024), Required]
        public string FilmURL { get; set; }

        // För att hämta director namn direkt, genre, similar film
        public virtual Director Director { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<SimilarFilm> SimilarFilms { get; set; }
    }
}
