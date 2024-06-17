#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Iktan.Ecommerce.Service.WebAPI/Iktan.Ecommerce.Service.WebAPI.csproj", "Iktan.Ecommerce.Service.WebAPI/"]
COPY ["Iktan.Ecommerce.App.DTO/Iktan.Ecommerce.App.DTO.csproj", "Iktan.Ecommerce.App.DTO/"]
COPY ["Iktan.Ecommerce.App.Interface/Iktan.Ecommerce.App.Interface.csproj", "Iktan.Ecommerce.App.Interface/"]
COPY ["Iktan.Ecommerce.Transversal.Common/Iktan.Ecommerce.Transversal.Common.csproj", "Iktan.Ecommerce.Transversal.Common/"]
COPY ["Iktan.Ecommerce.App.Main/Iktan.Ecommerce.App.Main.csproj", "Iktan.Ecommerce.App.Main/"]
COPY ["Iktan.Ecommerce.Domain.Entity/Iktan.Ecommerce.Domain.Entity.csproj", "Iktan.Ecommerce.Domain.Entity/"]
COPY ["Iktan.Ecommerce.Domain.Interface/Iktan.Ecommerce.Domain.Interface.csproj", "Iktan.Ecommerce.Domain.Interface/"]
COPY ["Iktan.Ecommerce.Domain.Core/Iktan.Ecommerce.Domain.Core.csproj", "Iktan.Ecommerce.Domain.Core/"]
COPY ["Iktan.Ecommerce.Infraestructure.Interface/Iktan.Ecommerce.Infraestructure.Interface.csproj", "Iktan.Ecommerce.Infraestructure.Interface/"]
COPY ["Iktan.Ecommerce.Infraestructure.Data/Iktan.Ecommerce.Infraestructure.Data.csproj", "Iktan.Ecommerce.Infraestructure.Data/"]
COPY ["Iktan.Ecommerce.Infraestructure.Repository/Iktan.Ecommerce.Infraestructure.Repository.csproj", "Iktan.Ecommerce.Infraestructure.Repository/"]
COPY ["Iktan.Ecommerce.Transversal.Logging/Iktan.Ecommerce.Transversal.Logging.csproj", "Iktan.Ecommerce.Transversal.Logging/"]
COPY ["Iktan.Ecommerce.Transversal.Mapper/Iktan.Ecommerce.Transversal.Mapper.csproj", "Iktan.Ecommerce.Transversal.Mapper/"]
RUN dotnet restore "./Iktan.Ecommerce.Service.WebAPI/Iktan.Ecommerce.Service.WebAPI.csproj"
COPY . .
WORKDIR "/src/Iktan.Ecommerce.Service.WebAPI"
RUN dotnet build "./Iktan.Ecommerce.Service.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Iktan.Ecommerce.Service.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Iktan.Ecommerce.Service.WebAPI.dll"]