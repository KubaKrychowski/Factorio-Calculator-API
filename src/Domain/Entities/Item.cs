using Domain.Common;
using Domain.Consts;

namespace Domain.Entities
{
    public class Item : BaseEntity
    {
        public virtual required string Name { get; set; }
        public virtual int Stars { get; set; }
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }
        public virtual string? IconUrl { get; set; }
        public virtual int PowerCost { get; set; }
        public virtual int Polution { get; set; }
        public virtual int EfficiencyPercentage { get; set; }
        public virtual int EfficiencyPerMinute { get; set; }
        public virtual ItemCategoryEnum CategoryId { get; set; }
    }
}
