# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files first for better layer caching
COPY Payroll.API/Payroll.API.csproj Payroll.API/
COPY HCM.Utilities/HCM.Utilities.csproj HCM.Utilities/
COPY HCM.Models/HCM.Models.csproj HCM.Models/
COPY HCM.DataAccess/HCM.DataAccess.csproj HCM.DataAccess/
COPY HCM.Services/HCM.Services.csproj HCM.Services/

# Restore dependencies
RUN dotnet restore Payroll.API/Payroll.API.csproj

# Copy remaining source code
COPY . .

# Publish application
WORKDIR /src/Payroll.API
RUN dotnet publish Payroll.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Final image
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Payroll.API.dll"]
