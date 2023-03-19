using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using VOD.Membership.Database.Entities;

namespace VOD.Membership.Database.Contexts
{
    public class VODContext : DbContext
    {
        // Skapar tabellerna
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<SimilarFilm> SimilarFilms { get; set; } = null!;
        public virtual DbSet<Director> Directors { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<FilmGenre> FilmGenres { get; set; } = null!;

        public VODContext(DbContextOptions<VODContext> options) : base(options) { 
        
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Composit keys
            builder.Entity<SimilarFilm>().HasKey(a => new { a.FilmId, a.SimilarFilmId });
            builder.Entity<FilmGenre>().HasKey(b => new { b.FilmId, b.GenreId });

            builder.Entity<Film>(entity =>
            {
                //Referera till relaterade filmer i similar films för varje film med ICollection<SimilarFilms>
           
                entity
                    .HasMany(d => d.SimilarFilms)
                    .WithOne(p => p.Film)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.Cascade);

                //config many to many 
           
                entity.HasMany(d => d.Genres)
                      .WithMany(p => p.Films)
                      .UsingEntity<FilmGenre>()
                      .ToTable("FilmGenres");
            });

            builder.Entity<Director>(entity =>
            {
                entity
                    .HasMany(f => f.Films)
                    .WithOne(p => p.Director)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}
