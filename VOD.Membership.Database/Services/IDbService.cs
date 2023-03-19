using System.Linq.Expressions;
using VOD.Membership.Database.Entities;

namespace VOD.Membership.Database.Services
{
    public interface IDbService
    {
        Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
            where TEntity : class, IEntity
            where TDto : class;
        Task<TReferenceEntity> AddReferenceAsync<TReferenceEntity, TDto>(TDto dto)
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class;
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity;
        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;
        bool DeleteReference<TReferenceEntity, TDto>(TDto dto)
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class;
        Task<List<TDto>> GetAsync<TEntity, TDto>()
            where TEntity : class, IEntity
            where TDto : class;
        Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class;
        Task<List<TDto>> GetReferenceAsync<TReferenceEntity, TDto>()
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class;
        string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Include<TEntity>() where TEntity : class, IEntity;
        Task IncludeReferenceAsync<TReferenceEntity>() where TReferenceEntity : class, IReferenceEntity;
        Task<bool> SaveChangesAsync();
        Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class;
        Task<TDto> SingleRefAsync<TReferenceEntity, TDto>(Expression<Func<TReferenceEntity, bool>> expression)
            where TReferenceEntity : class, IReferenceEntity
            where TDto : class;
        void Update<TEntity, TDto>(int id, TDto dto)
            where TEntity : class, IEntity
            where TDto : class;
    }
}