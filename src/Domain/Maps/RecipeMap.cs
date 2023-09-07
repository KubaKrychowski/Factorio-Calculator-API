using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Maps
{
    internal class RecipeMap : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasIndex(r => r.Id).IsUnique();
            builder.HasQueryFilter(r => r.IsDeleted == false);

            builder.Property(r => r.ProductionTime).IsRequired();
            builder.Property(r => r.ItemExternalId).IsRequired();

            builder.HasMany(r => r.Ingredients);
        }
    }
}
