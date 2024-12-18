FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /api
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/api
COPY ["OnlineShopWebApi/OnlineShopWebApi.csproj", "OnlineShopWebApi/"]
COPY ["OnlineShop.Db/OnlineShop.Db.csproj", "OnlineShop.Db/"]
COPY ["OnlineShop.SecretManager/OnlineShop.SecretManager.csproj", "OnlineShop.SecretManager/"]
COPY ["OnlineShopWebApp/OnlineShopWebApp.csproj", "OnlineShopWebApp/"]
RUN dotnet restore "./OnlineShopWebApi/OnlineShopWebApi.csproj"
COPY . .
WORKDIR "/src/api/OnlineShopWebApi"
RUN dotnet build "./OnlineShopWebApi.csproj" -c $BUILD_CONFIGURATION -o /api/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineShopWebApi.csproj" -c $BUILD_CONFIGURATION -o /api/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /api
COPY ["app.env", "/api/"]
COPY ["OnlineShop.pfx", "/home/app/.aspnet/https/OnlineShop.pfx"]
COPY --from=publish /api/publish .

ENTRYPOINT ["dotnet", "OnlineShopWebApi.dll"]