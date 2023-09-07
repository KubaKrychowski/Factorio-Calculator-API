using Contracts.Recipes.Responses;
using Domain.Consts;

namespace Contracts.Items.Responses
{
    public class GetItemResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required ItemCategoryEnum CategoryId { get; set; }
        public int Stars { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int PowerCost { get; set; }
        public int Polution { get; set; }
        public string? IconUrl { get; set; }
        public ItemEfficiencyResponseDto? Efficiency { get; set; }
        public List<GetItemRecipeResponseDto>? Recipes { get; set; }
    }

    public class ItemEfficiencyResponseDto
    {
        public int EfficiencyPercentage { get; set; }
        public int EfficiencyPerMinute { get; set; }
    }
}
