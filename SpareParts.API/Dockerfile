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


#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["app.csproj", "./"]
#RUN dotnet restore "./app.csproj"
#COPY . .
#RUN dotnet build "app.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "app.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "app.dll"]

