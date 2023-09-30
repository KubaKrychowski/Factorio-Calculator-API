using Domain.Common;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IBaseEntityRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        Task<TBaseEntity> GetByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);

        Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    }

    public abstract class BaseEntityRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        protected readonly DbContext _dbContext;

        protected BaseEntityRepository(DbContext dbContext) => _dbContext = dbContext;

        public async Task<TBaseEntity> GetByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TBaseEntity>().SingleOrDefaultAsync(e => e.ExternalId == externalId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        {

        }

    }
}
