FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY OnlineShop.Migrations/OnlineShop.Migrations.csproj OnlineShop.Migrations/
COPY OnlineShop.Data/OnlineShop.Data.csproj OnlineShop.Data/

RUN dotnet restore OnlineShop.Migrations/OnlineShop.Migrations.csproj

COPY . .

WORKDIR /src/OnlineShop.Migrations
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "OnlineShop.Migrations.dll"]
