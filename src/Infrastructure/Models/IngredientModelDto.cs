namespace Infrastructure.Models
{
    public class IngredientModelDto
    {
        public Guid ExternalId { get; set; }
        public Guid ItemExternalId { get; set; }
        public int Amount { get; set; }
    }
}
