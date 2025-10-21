# Imagen base para ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar el csproj y restaurar dependencias
COPY ["Qualifier.Api.csproj", "./"]
RUN dotnet restore "./Qualifier.Api.csproj"

# Copiar el resto del código y compilar
COPY . .
RUN dotnet publish "./Qualifier.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final: solo runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Configurar el puerto que Railway espera
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Qualifier.Api.dll"]
