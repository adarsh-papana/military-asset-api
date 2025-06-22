# Use the official .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Military_Asset_Management_System/*.csproj ./Military_Asset_Management_System/
RUN dotnet restore Military_Asset_Management_System/Military_Asset_Management_System.csproj

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet publish Military_Asset_Management_System/Military_Asset_Management_System.csproj -c Release -o /app/publish

# Use the official .NET 8 runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose port (Railway uses $PORT env variable, but EXPOSE is still good practice)
EXPOSE 5000

# Set the entrypoint
ENTRYPOINT ["dotnet", "Military_Asset_Management_System.dll"]