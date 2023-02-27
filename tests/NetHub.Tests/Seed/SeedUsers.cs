using NetHub.Data.SqlServer.Entities;

namespace NetHub.Tests.Seed;

public static class SeedUsers
{
    public static readonly User[] Default =
    {
        new()
        {
            Id = 1,
            Email = "geektweeker@gmail.com",
            NormalizedEmail = "GEEKTWEEKER@GMAIL.COM",
            FirstName = "Vlad",
            LastName = "Fit",
            UserName = "tweeker",
            NormalizedUserName = "TWEEKER"
        },
        new()
        {
            Id = 2,
            Email = "bogdana@gmail.com",
            NormalizedEmail = "BOGDANA@GMAIL.COM",
            FirstName = "Bogdana",
            LastName = "Fit",
            UserName = "f.dana19",
            NormalizedUserName = "F.DANA19"
        }
    };
}