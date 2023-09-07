using Domain.Consts;
using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IItemCategoryRepository
    {
        public Task<ItemCategory> GetByIdAsync(ItemCategoryEnum id, CancellationToken cancellationToken);
    }
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly DataContext _dataContext;

        public ItemCategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ItemCategory> GetByIdAsync(ItemCategoryEnum id, CancellationToken cancellationToken)
        {
            return await _dataContext.ItemCategory.FirstAsync(c => c.Id == id, cancellationToken);
        }
    }
}
