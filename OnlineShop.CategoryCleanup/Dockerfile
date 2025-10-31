FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./OnlineShop.CategoryCleanup/*.csproj ./OnlineShop.CategoryCleanup/
COPY ./OnlineShop.Core/*.csproj ./OnlineShop.Core/
COPY ./OnlineShop.Data/*.csproj ./OnlineShop.Data/

RUN dotnet restore ./OnlineShop.CategoryCleanup/OnlineShop.CategoryCleanup.csproj

COPY . .

RUN dotnet publish ./OnlineShop.CategoryCleanup/OnlineShop.CategoryCleanup.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "OnlineShop.CategoryCleanup.dll"]
