using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Maps
{
    public class RecipeIngredientMap : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.HasIndex(ri => ri.Id).IsUnique();
            builder.HasQueryFilter(ri => ri.IsDeleted == false);

            builder.Property(ri => ri.ItemExternalId).IsRequired();
            builder.Property(ri => ri.RecipeExternalId).IsRequired();
        }
    }
}
