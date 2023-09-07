using Contracts.Recipes.Requests;
using Domain.Consts;

namespace Contracts.Items.Requests
{
    public class AddItemRequestDto
    {
        public required string Name { get; set; }
        public int Stars { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int PowerCost { get; set; }
        public int Polution { get; set; }
        public ItemCategoryEnum Category { get; set; }
        public AddRecipeRequestDto? Recipe { get; set; }
    }
}
