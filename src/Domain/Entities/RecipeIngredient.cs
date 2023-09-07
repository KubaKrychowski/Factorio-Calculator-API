using Domain.Common;

namespace Domain.Entities
{
    public class RecipeIngredient : BaseEntity
    {
        public virtual Guid ItemExternalId { get; set; }
        public virtual Guid RecipeExternalId { get; set; }
        public virtual int Amount { get; set; }
    }
}
