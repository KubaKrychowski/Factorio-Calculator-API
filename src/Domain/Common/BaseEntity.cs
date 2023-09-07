using Infrastructure.TimeProvider;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public BaseEntity()
        {
            ExternalId = Guid.NewGuid();
            CreatedAt = TimeProvider.Now;
            IsDeleted = false;
        }
    }
}
