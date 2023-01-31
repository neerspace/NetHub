namespace NetHub.Core.Exceptions;

public static class DateTimeExtension
{
    public static DateTimeOffset ToUtcOffset(this DateTime dateTime) => new(dateTime, TimeSpan.Zero);
}