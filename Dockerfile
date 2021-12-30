# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./KubakLandingApi/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./KubakLandingApi/* ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build-env /app/out .
COPY heroku_run.sh .

ENTRYPOINT ["dotnet", "KubakLandingApi.dll"]
