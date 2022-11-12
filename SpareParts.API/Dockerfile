# First-stage base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /source

# Copy everything
COPY *.csproj

# Restore as distinct layers - restore dependencies
RUN dotnet restore

# Build and publish a release
COPY . .
RUN dotnet publish -c Release -o /app 

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SpareParts.API.dll"]


