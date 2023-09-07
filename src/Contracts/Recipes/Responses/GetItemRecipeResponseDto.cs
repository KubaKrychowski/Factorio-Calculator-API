using Infrastructure.Models;

namespace Contracts.Recipes.Responses
{
    public class GetItemRecipeResponseDto
    {
        public Guid ExternalId { get; set; }
        public List<IngredientModelDto> Ingredients { get; set; }
        public int ProductionTime { get; set; }
    }
}
