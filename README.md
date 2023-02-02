# NetHub Backend

## Getting Started

1. Ensure that you have been installed [`.NET 7 SDK`] and [`ASP.NET Core 7 Runtime`].
2. Go to the project root directory (where file `.sln` stored): \
   `cd ./nethub-backend`.
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

To make your life easier, we have configured these rules in [editorconfig].
If you are using `JetBrains Rider`, it will automatically apply rules from this file.
For other IDEs, you may need to install a plugin to enable our styling support.

## How to configure custom local domains

### MacOS

1. `sudo nano /etc/hosts`
2. Add there next lines:
```
127.0.0.1       nethub.local
127.0.0.1       api.nethub.local
127.0.0.1       admin.nethub.local
127.0.0.1       admin-api.nethub.local
```
3. `^O` then `Enter` to save changes. `^X` to exit
4. `brew install nginx` to install nginx
5. `brew services start nginx` to start nginx
6. Then open next file: `sudo nano /usr/local/etc/nginx/nginx.conf` (`⌘ shift .` to show hidden files if you want to open it in the text editor)
7. Remove all (if you didn't use nginx before) and type next lines:
```
worker_processes  1;

events {
    worker_connections  1024;
}

http {
    # NetHub API
    server {
        listen 80;
        server_name api.nethub.local;
        location / {
            proxy_pass https://localhost:9010;
        }
    }

    # NetHub APP
    server {
        listen 80;
        server_name nethub.local;
        location / {
            proxy_pass http://localhost:9000;
        }
    }

    # NetHub ADMIN API
    server {
        listen 80;
        server_name admin-api.nethub.local;
        location / {
            proxy_pass https://localhost:9110;
        }
    }

    # NetHub ADMIN
    server {
        listen 80;
        server_name admin.nethub.local;
        location / {
            proxy_pass http://localhost:9100;
        }
    }
}
```
8. Restart nginx server: `brew services restart nginx`

[`.NET 7 SDK`]: https://dotnet.microsoft.com/en-us/download/dotnet/7.0
[`ASP.NET Core 7 Runtime`]: https://dotnet.microsoft.com/en-us/download/dotnet/7.0
[the Google styleguide for C#]: https://google.github.io/styleguide/csharp-style.html
[editorconfig]: https://github.com/NeerSpace/nethub-backend/blob/development/.editorconfig

