using Application.models;
using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IItemsRepository : IBaseEntityRepository<Item>
    {
        public Task<List<ItemModelDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<Guid> AddItemAsync(Item item, CancellationToken cancellationToken);
        public Task<List<Item>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
    public class ItemsRepository : BaseEntityRepository<Item>, IItemsRepository
    {
        private readonly IRecipeRepository _recipeRepository;

        public ItemsRepository(DataContext dbContext, IRecipeRepository recipeRepository) : base(dbContext)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<List<ItemModelDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            IQueryable<ItemModelDto> resultsQuery = _dbContext.Items
                .GroupJoin(
                _dbContext.Recipes,
                i => i.ExternalId,
                r => r.ItemExternalId,
                (_item, _recipes) => new ItemModelDto() { Item = _item, Recipe = _recipes.ToList() });

            return await resultsQuery.ToListAsync(cancellationToken);
        }

        public async Task<Guid> AddItemAsync(Item item, CancellationToken cancellationToken)
        {
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return item.ExternalId;
        }

        public async Task<List<Item>> GetByIdsAsync(List<Guid> externalIds, CancellationToken cancellationToken)
        {
            return await _dbContext.Items.Where(i => externalIds.Contains(i.ExternalId)).ToListAsync(cancellationToken);
        }
    }
}
