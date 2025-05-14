# Sample ASP.NET Core Store

## Docker Configuration

### Start a postgres instance
```
docker run --name my_postgres -p 5438:5432 -e POSTGRES_PASSWORD=postgres -d postgres
```
[Oficial Documentation Docker](https://hub.docker.com/_/postgres)

## Redis Configuration
```
docker run --name my-redis -p 6379:6379 -d redis
```

## Dotnet Configuration

### Installing the tools
```
dotnet tool install --global dotnet-ef
```

```
dotnet tool update --global dotnet-ef
```

[Oficial Documentation Microsoft](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Using the tools

#### Adding the first migration
```
dotnet ef migrations add initial -c ApplicationDbContext --project ./ShopDevelop.Persistence -s "E:\Projects\C#\ASP.NET Core\ShopDeveloped\ShopDevelop.WebApi"
```
```
dotnet ef migrations add initial -c ApplicationDbContext --project ./ShopDevelop.Persistence -s "/home/renx1ee/Documents/Projects/C#/ASP.Net Core/ShopDeveloped/ShopDevelop.WebApi"
```

#### Migration update
```
dotnet ef database update -c ApplicationDbContext --project ./ShopDevelop.Persistence -s "E:\Projects\C#\ASP.NET Core\ShopDeveloped\ShopDevelop.WebApi" --verbose
```
```
dotnet ef database update -c ApplicationDbContext --project ./ShopDevelop.Persistence -s "/home/renx1ee/Documents/Projects/C#/ASP.Net Core/ShopDeveloped/ShopDevelop.WebApi"
```
