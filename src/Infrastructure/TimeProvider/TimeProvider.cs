namespace Infrastructure.TimeProvider
{
    public class TimeProvider
    {
        private static readonly Func<DateTimeOffset> TimeSource = (Func<DateTimeOffset>)(() => DateTimeOffset.UtcNow);
        public static DateTimeOffset Now = TimeProvider.TimeSource();
    }
}
