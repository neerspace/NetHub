using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NetHub.Data.SqlServer.Conversions;

public class DateTimeOffsetConvertor : ValueConverter<DateTimeOffset, DateTime>
{
    public DateTimeOffsetConvertor() : base(
        doff => doff.UtcDateTime,
        dt => new DateTimeOffset(dt, TimeSpan.Zero)
    )
    { }
}