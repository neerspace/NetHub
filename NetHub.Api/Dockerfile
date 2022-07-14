FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NetHub.Api/NetHub.Api.csproj", "NetHub.Api/"]
COPY ["NetHub.Application/NetHub.Application.csproj", "NetHub.Application/"]
COPY ["NetHub.Core/NetHub.Core.csproj", "NetHub.Core/"]
COPY ["NetHub.Data.SqlServer/NetHub.Data.SqlServer.csproj", "NetHub.Data.SqlServer/"]
COPY ["NetHub.Infrastructure/NetHub.Infrastructure.csproj", "NetHub.Infrastructure/"]
RUN dotnet restore "NetHub.Api/NetHub.Api.csproj"
COPY . .
WORKDIR "/src/NetHub.Api"
RUN dotnet build "NetHub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetHub.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetHub.Api.dll"]
