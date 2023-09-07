namespace Contracts.Recipes.Requests
{
    public class AddRecipeRequestDto
    {
        public Guid FinalItemId { get; set; }
        public required IEnumerable<AddRecipeIngredientDto> Ingredients { get; set; }
        public int ProductionTime { get; set; }
    }

    public class AddRecipeIngredientDto
    {
        public Guid ItemExternalId { get; set; }
        public int Amount { get; set; }
    }
}
