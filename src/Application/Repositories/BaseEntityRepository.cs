using Domain.Common;
using Domain.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IBaseEntityRepository<EntityType>
    {
        public Task<EntityType?> GetByIdAsync(Guid externalId, CancellationToken cancellationToken);
        public Task SetAsDeleted<TEntity>(Guid externalId) where TEntity : BaseEntity;
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }

    public abstract class BaseEntityRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataContext _dbContext;
        
        protected BaseEntityRepository(DataContext dbContext) => _dbContext = dbContext;

        public async Task<TEntity?> GetByIdAsync(Guid externalId, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.ExternalId == externalId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SetAsDeleted<EntityType>(Guid externalId) where EntityType : BaseEntity
        {
            var entity = await _dbContext.Set<EntityType>().SingleOrDefaultAsync(e => e.ExternalId == externalId);

            if (entity == null)
            {
                throw new NotFoundException(new[] { typeof(EntityType).Name, externalId.ToString() });
            }

            entity.IsDeleted = true;

            _dbContext.Set<EntityType>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
