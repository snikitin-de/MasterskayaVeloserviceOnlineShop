FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/app
COPY ["OnlineShopWebApp/OnlineShopWebApp.csproj", "OnlineShopWebApp/"]
COPY ["OnlineShop.SecretManager/OnlineShop.SecretManager.csproj", "OnlineShop.SecretManager/"]
RUN dotnet restore "./OnlineShopWebApp/OnlineShopWebApp.csproj"
COPY . .
WORKDIR "/src/app/OnlineShopWebApp"
RUN dotnet build "./OnlineShopWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineShopWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["app.env", "/app/"]
COPY ["OnlineShop.pfx", "/home/app/.aspnet/https/OnlineShop.pfx"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShopWebApp.dll"]