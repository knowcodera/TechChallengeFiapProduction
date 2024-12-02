# Base image for .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["src/ProductionApi/ProductionApi.csproj", "ProductionApi/"]
RUN dotnet restore "ProductionApi/ProductionApi.csproj"

# Copy only the source code for ProductionApi
COPY src/ProductionApi/ ./ProductionApi/
WORKDIR "/src/ProductionApi"

# Build the main application
RUN dotnet build "ProductionApi.csproj" -c Release -o /app/build

# Publish the main application
FROM build AS publish
RUN dotnet publish "ProductionApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8580
ENTRYPOINT ["dotnet", "ProductionApi.dll"]
