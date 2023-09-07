using Domain.Consts;
using Domain.Entities;
using Infrastructure.EntitiesUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Maps
{
    public class ItemCategoryMap : IEntityTypeConfiguration<ItemCategory>
    {
        public void Configure(EntityTypeBuilder<ItemCategory> builder)
        {
            builder.HasData(
            SeedEnumToTable.ConvertEnumToList<ItemCategoryEnum>());
        }
    }
}
