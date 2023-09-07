using Infrastructure.Entities;

namespace Infrastructure.EntitiesUtils
{
    public static class SeedEnumToTable
    {
        public static List<DictionaryEntity<TEnum>> ConvertEnumToList<TEnum>() where TEnum : Enum
        {
            List<DictionaryEntity<TEnum>> enumList = new List<DictionaryEntity<TEnum>>();

            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                var dictionaryEntity = new DictionaryEntity<TEnum>()
                {
                    Id = value,
                    Name = value.ToString()
                };

                enumList.Add(dictionaryEntity);
            }

            return enumList;
        }
    }
}
