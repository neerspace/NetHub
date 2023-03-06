# NetHub

## Projects

- **NetHub API** `/app/NetHub.Api`
- **NetHub React UI** `/app/NetHub.UI`
- **NetHub Admin API** `/admin/NetHub.Admin,Api`
- **NetHub Admin Angular UI** `/admin/NetHub.Admin.UI`

## Getting Started

1. Ensure that you have been installed [`.NET 7 SDK`] and [`ASP.NET Core 7 Runtime`].
2. Go to the project root directory (where file `.sln` stored): \
   `cd ./nethub`.
3. Restore `NuGet` packages using IDE UI or the following command: \
   `dotnet restore`
4. Run `NetHub.Api` project: \
   `cd ./NetHub.Api` then
   `dotnet run` or `dotnet watch`


## API Reference

You can find explore API using Swagger UI: https://localhost:7002/swagger

Or alternative documentation ReDoc: https://localhost:7002/docs-v1


## EntityFramework Migrations

### CMD/Terminal

- Add new migration: \
   `dotnet ef migrations add <migration_name>`
- Remove last migration: \
   `dotnet ef migrations remove`
- Update database to last migration: \
   `dotnet ef database update`
- Rollback database to specific migration: \
   `dotnet ef database update <migration_name>`

### PowerShell

- Add new migration: \
  `Add-Migration <migration_name>`
- Remove last migration: \
  `Remove-Migration`
- Update database to last migration: \
  `Update-Database`
- Rollback database to specific migration: \
  `Update-Database <migration_name>`

## Code Style

> Please, follow [the Google styleguide for C#].

To make your life easier, we have configured these rules in [.editorconfig].
If you are using `JetBrains Rider`, it will automatically apply rules from this file.
For other IDEs, you may need to install a plugin to enable our styling support.

## How to configure custom local domains

Follow this article to find and update [hosts file on Windows]. \
If you're using MacOS you can also find [hosts file on MacOS].

Then add next lines to that file:
```
127.0.0.1       nethub.local
127.0.0.1       api.nethub.local
127.0.0.1       admin.nethub.local
127.0.0.1       admin-api.nethub.local
```

[`.NET 7 SDK`]: https://dotnet.microsoft.com/en-us/download/dotnet/7.0
[`ASP.NET Core 7 Runtime`]: https://dotnet.microsoft.com/en-us/download/dotnet/7.0
[the Google styleguide for C#]: https://google.github.io/styleguide/csharp-style.html
[.editorconfig]: https://github.com/NeerSpace/nethub-backend/blob/development/.editorconfig
[hosts file on Windows]: https://www.howtogeek.com/27350/beginner-geek-how-to-edit-your-hosts-file/
[hosts file on MacOS]: https://www.nexcess.net/help/how-to-find-the-hosts-file-on-my-mac/
