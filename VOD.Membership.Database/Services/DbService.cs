using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VOD.Membership.Database.Contexts;
using VOD.Membership.Database.Entities;

namespace VOD.Membership.Database.Services
{
    public class DbService : IDbService
    {
        private readonly VODContext _db;
        private readonly IMapper _mapper;

        public DbService(VODContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Hämta tabellen av TEntity och returnera som en lista av DTOs
        // Get All
        public async Task<List<TDto>> GetAsync<TEntity, TDto>()
            where TEntity : class, IEntity
            where TDto : class
        {
            var entities = await _db.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        // Get utifrån expression , ex id<5
        public async Task<List<TDto>> GetAsync<TEntity, TDto>(
            Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entities = await _db.Set<TEntity>().Where(expression).ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        // Private Get single som anropas av den publika
        private async Task<TEntity?> SingleAsync<TEntity>(
        Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity =>
        await _db.Set<TEntity>().SingleOrDefaultAsync(expression);

        // Get single, anropar den privata verisionen
        public async Task<TDto> SingleAsync<TEntity, TDto>(
            Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = await SingleAsync(expression);
            return _mapper.Map<TDto>(entity);
        }

        // Konverterar en DTO som skickats till APIet till en entitet som sparas i databasen
        public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class, IEntity
        where TDto : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        // Sparar förändringarna i databasen, returnerar positivt som lyckat
        public async Task<bool> SaveChangesAsync() =>
            await _db.SaveChangesAsync() >= 0;

        // Skapa en URI  namnet/id
        public string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity
            => $"/{typeof(TEntity).Name.ToLower()}s/{entity.Id}";

        // Update one
        public void Update<TEntity, TDto>(int id, TDto dto)
        where TEntity : class, IEntity
        where TDto : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            entity.Id = id;
            _db.Set<TEntity>().Update(entity);
        }

        // Returns true if elements exist
        public async Task<bool> AnyAsync<TEntity>(
        Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity =>
            await _db.Set<TEntity>().AnyAsync(expression);

        // Delete one
        public async Task<bool> DeleteAsync<TEntity>(int id)
        where TEntity : class, IEntity
        {
            try
            {
                var entity = await SingleAsync<TEntity>(e => e.Id.Equals(id));
                if (entity is null) return false;
                _db.Remove(entity);
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        // Fyller navigerings properties med data, tex film har en navprop som heter director, fyller den med directors namn
        // Kan anropas flera gånger tex vid kopplingstabbell, först med film sen med filmgenre
        public void Include<TEntity>() where TEntity : class, IEntity
        {
            var propertyNames = _db.Model.FindEntityType(
                typeof(TEntity))?.GetNavigations()
                .Select(e => e.Name);

            if (propertyNames is null) return;

            foreach (var name in propertyNames)
                _db.Set<TEntity>().Include(name).Load();
        }


        //Kopplingstabeller

        

        // Get all
        public async Task<List<TDto>> GetReferenceAsync<TReferenceEntity, TDto>()
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class
        {
            var entities = await _db.Set<TReferenceEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        // private get single
        private async Task<TReferenceEntity?> SingelAsync<TReferenceEntity>(
        Expression<Func<TReferenceEntity, bool>> expression)
        where TReferenceEntity : class, IReferenceEntity =>
            await _db.Set<TReferenceEntity>().SingleOrDefaultAsync(expression);

        // get single anropar privata
        public async Task<TDto> SingleRefAsync<TReferenceEntity, TDto>(
        Expression<Func<TReferenceEntity, bool>> expression)
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class
        {
            var entity = await SingelAsync(expression);
            return _mapper.Map<TDto>(entity);
        }

        // Add
        public async Task<TReferenceEntity> AddReferenceAsync<TReferenceEntity, TDto>(TDto dto)
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class
        {
            var entity = _mapper.Map<TReferenceEntity>(dto);
            await _db.Set<TReferenceEntity>().AddAsync(entity);
            return entity;
        }

        // Delete
        public bool DeleteReference<TReferenceEntity, TDto>(TDto dto)
                where TReferenceEntity : class, IReferenceEntity
                where TDto : class
        {
            try
            {
                var entity = _mapper.Map<TReferenceEntity>(dto);
                if (entity == null)
                {
                    return false;
                }
                _db.Remove(entity);
            }
            catch { throw; }

            return true;
        }

        // Include
        public async Task IncludeReferenceAsync<TReferenceEntity>()
        where TReferenceEntity : class, IReferenceEntity
        {
            for (int i = 0; i < 1; i++)
            {
                var propertyNames = _db.Model.FindEntityType(typeof(TReferenceEntity))?.GetNavigations().Select(e => e.Name);
                if (propertyNames != null)
                {
                    foreach (var name in propertyNames)
                    {
                        _db.Set<TReferenceEntity>().Include(name).Load();
                    }
                }
            }



        }
    }
}
