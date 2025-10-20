FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY OnlineShop.Api/OnlineShop.Api.csproj OnlineShop.Api/
COPY OnlineShop.Core/OnlineShop.Core.csproj OnlineShop.Core/
COPY OnlineShop.Data/OnlineShop.Data.csproj OnlineShop.Data/

RUN dotnet restore OnlineShop.Api/OnlineShop.Api.csproj

COPY . .

WORKDIR /src/OnlineShop.Api
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "OnlineShop.Api.dll"]
