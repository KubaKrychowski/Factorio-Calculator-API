using Contracts.Recipes.Requests;
using Domain.Consts;

namespace Contracts.Items.Requests
{
    public class UpdateItemRequestDto
    {
        public string? Name { get; set; }
        public int? Stars { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? PowerCost { get; set; }
        public int Polution { get; set; }
        public int EfficiencyPercentage { get; set; }
        public int EfficiencyPerMinute { get; set; }
        public ItemCategoryEnum? Category { get; set; }
        public required IEnumerable<AddRecipeIngredientDto> Ingredients { get; set; }
        public int? ProductionTime { get; set; }
    }
}
