using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Repositories
{
    public interface IItemsRepository : IBaseEntityRepository<Item>
    {
        Task<List<Item>> GetAllAsync(CancellationToken cancellationToken);
    }

    public class ItemsRepository : BaseEntityRepository<Item>,
        IItemsRepository
    {
        private readonly DataContext _dbContext;
        public ItemsRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Item>> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Item>().AsQueryable();

            return await query.ToListAsync(cancellationToken);
        }
    }
}
