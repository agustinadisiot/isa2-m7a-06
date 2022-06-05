# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.301 AS build-env

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.5
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "MinTur.WebApi.dll"]