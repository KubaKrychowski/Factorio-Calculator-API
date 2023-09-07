using Domain.Common;

namespace Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public virtual Guid ItemExternalId { get; set; }
        public virtual int ProductionTime { get; protected set; }
        public virtual ICollection<RecipeIngredient> Ingredients { get; protected set; } = new List<RecipeIngredient>();

        public Recipe()
        {

        }

        public Recipe(Guid itemExternalId, int productionTime, IEnumerable<RecipeIngredient> ingredients)
        {
            ItemExternalId = itemExternalId;
            Ingredients = ingredients.ToList();
            ProductionTime = productionTime;
        }

        public Recipe SetProductionTime(int productionTime)
        {
            ProductionTime = productionTime;
            return this;
        }

        public Recipe SetIngredients(List<RecipeIngredient> ingredients)
        {
            Ingredients = ingredients;
            return this;
        }
    }
}
