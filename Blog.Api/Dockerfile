#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Blog.Api/Blog.Api.csproj", "Blog.Api/"]
COPY ["Blog.Data/Blog.Data.csproj", "Blog.Data/"]
COPY ["Blog.Models/Blog.Models.csproj", "Blog.Models/"]
RUN dotnet restore "./Blog.Api/./Blog.Api.csproj"
COPY . .
WORKDIR "/src/Blog.Api"
RUN dotnet build "./Blog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Blog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Api.dll"]