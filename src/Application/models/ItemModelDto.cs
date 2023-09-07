using Domain.Entities;

namespace Application.models
{
    public class ItemModelDto
    {
        public required Item Item { get; set; }
        public required ICollection<Recipe> Recipe { get; set; }
    }
}
