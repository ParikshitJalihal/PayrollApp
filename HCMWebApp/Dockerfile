# ==========================
# Runtime Image
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

# ==========================
# Build Image
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY HCMWebApp/HCMWebApp.csproj HCMWebApp/
COPY HCM.Utilities/HCM.Utilities.csproj HCM.Utilities/
COPY HCM.Models/HCM.Models.csproj HCM.Models/
COPY HCM.DataAccess/HCM.DataAccess.csproj HCM.DataAccess/
COPY HCM.Services/HCM.Services.csproj HCM.Services/

# Restore dependencies
RUN dotnet restore HCMWebApp/HCMWebApp.csproj

# Copy all source files
COPY . .

# Build & Publish
WORKDIR /src/HCMWebApp

RUN dotnet publish HCMWebApp.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ==========================
# Final Image
# ==========================
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "HCMWebApp.dll"]
