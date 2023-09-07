using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IRecipeRepository : IBaseEntityRepository<Recipe>
    {
        public Guid AddRecipeAsync(Recipe recipe);
        public Task<List<Recipe>> GetAllAsync(CancellationToken cancellationToken);
        public Task<List<Recipe>> GetByItemExternalIdAsync(Guid itemExternalId, CancellationToken cancellationToken);
    }

    public class RecipeRepository : BaseEntityRepository<Recipe>, IRecipeRepository
    {
        private readonly DataContext _dbContext;

        public RecipeRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddRecipeAsync(Recipe recipe)
        {
            _dbContext.Recipes.Add(recipe);
            return recipe.ExternalId;
        }

        public async Task<List<Recipe>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Recipes.ToListAsync(cancellationToken);
        }

        public async Task<List<Recipe>> GetByItemExternalIdAsync(Guid itemExternalId, CancellationToken cancellationToken)
        {
            return await _dbContext.Recipes.Include(recipe => recipe.Ingredients).Where(recipe => recipe.ItemExternalId == itemExternalId).ToListAsync(cancellationToken);
        }
    }
}
