# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY HCMWebApp/HCMWebApp.csproj HCMWebApp/
COPY HCM.Utilities/HCM.Utilities.csproj HCM.Utilities/
COPY HCM.Models/HCM.Models.csproj HCM.Models/
COPY HCM.DataAccess/HCM.DataAccess.csproj HCM.DataAccess/
COPY HCM.Services/HCM.Services.csproj HCM.Services/
RUN dotnet restore HCMWebApp/HCMWebApp.csproj

# Copy all source
COPY . .
WORKDIR /src/HCMWebApp
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HCMWebApp.dll"]
