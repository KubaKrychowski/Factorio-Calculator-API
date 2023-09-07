using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class DictionaryEntity<TEnum> where TEnum : Enum
    {
        [Key]
        public required TEnum Id { get; set; }
        public required string Name { get; set; }
    }
}
