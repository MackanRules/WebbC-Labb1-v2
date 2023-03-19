using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VOD.Membership.Database.Entities
{
    public class Director : IEntity
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }

        //För att hämta alla filmer från en instruktör
        public virtual ICollection<Film> Films { get; set; }

    }
}
