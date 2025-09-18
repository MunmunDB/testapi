# Stage 1: Build 
FROM mcr.microsoft.com/dotnet/core/sdk:2.0 AS build
WORKDIR /src

COPY *.sln ./
COPY Api/*.csproj ./Api/
RUN dotnet restore "FundsApi/Api.csproj"

COPY . .
WORKDIR /src/Api
RUN dotnet publish \
    --configuration Release \
    --output /app/publish

# Stage 2: Run 
FROM mcr.microsoft.com/dotnet/core/aspnet:2.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Expose the HTTP port 
EXPOSE 80

# Launch 
ENTRYPOINT ["dotnet", "Api.dll"]