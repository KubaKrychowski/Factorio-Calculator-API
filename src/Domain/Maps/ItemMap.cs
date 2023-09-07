using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Maps
{
    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasIndex(i => i.Id).IsUnique();
            builder.HasQueryFilter(i => i.IsDeleted == false);
            builder.Property(i => i.CategoryId).IsRequired();
        }
    }
}
