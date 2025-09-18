# ─── BUILD STAGE ────────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/core/sdk:2.0 AS build

WORKDIR /src

# Copy solution and project files
COPY *.sln ./
COPY FundsApi/*.csproj ./FundsApi/

# Restore only projects needed for build
RUN dotnet restore "FundsApi/Api.csproj"

# Copy all source code and publish
COPY . .
WORKDIR /src/FundsApi
RUN dotnet publish -c Release -o /app/publish

# ─── RUNTIME STAGE ──────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/core/aspnet:2.0 AS runtime

WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish ./

# Set production environment and listen on port 80
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Expose port 80 to the host
EXPOSE 80

# Create a non-root user and switch to it
RUN adduser --disabled-password --gecos "" appuser \
 && chown -R appuser /app
USER appuser

# Healthcheck (adjust the path to an actual endpoint your API exposes)
HEALTHCHECK --interval=30s --timeout=5s --start-period=30s \
  CMD curl --fail http://localhost/api/health || exit 1

# Launch the API
ENTRYPOINT ["dotnet", "FundsApi.dll"]
