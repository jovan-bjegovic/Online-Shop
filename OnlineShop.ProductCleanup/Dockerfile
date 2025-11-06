FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./OnlineShop.ProductCleanup/*.csproj ./OnlineShop.ProductCleanup/
COPY ./OnlineShop.Core/*.csproj ./OnlineShop.Core/
COPY ./OnlineShop.Data/*.csproj ./OnlineShop.Data/

RUN dotnet restore ./OnlineShop.ProductCleanup/OnlineShop.ProductCleanup.csproj

COPY . .

RUN dotnet publish ./OnlineShop.ProductCleanup/OnlineShop.ProductCleanup.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .
COPY ./OnlineShop.ProductCleanup/appsettings.json ./appsettings.json
ENTRYPOINT ["dotnet", "OnlineShop.ProductCleanup.dll"]
