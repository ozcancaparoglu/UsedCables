#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/ProductService/ProductService.Api/ProductService.Api.csproj", "src/Services/ProductService/ProductService.Api/"]
COPY ["src/Services/ProductService/ProductService.Application/ProductService.Application.csproj", "src/Services/ProductService/ProductService.Application/"]
COPY ["src/Infrastructure/UsedCables.Infrastructure/UsedCables.Infrastructure.csproj", "src/Infrastructure/UsedCables.Infrastructure/"]
COPY ["src/Services/ProductService/ProductService.Domain/ProductService.Domain.csproj", "src/Services/ProductService/ProductService.Domain/"]
RUN dotnet restore "./src/Services/ProductService/ProductService.Api/ProductService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/ProductService/ProductService.Api"
RUN dotnet build "./ProductService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.Api.dll"]