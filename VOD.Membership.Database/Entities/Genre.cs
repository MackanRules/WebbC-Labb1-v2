using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VOD.Membership.Database.Entities
{
    public class Genre : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        //För att hämta alla filmer från denna genre 
        public virtual ICollection<Film> Films { get; set; }
    }
}
