#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#Install Azure CLI so we can use DefaultAzureCredential and AAD for authentication
RUN apt update
RUN apt install curl -y
RUN curl -sL https://aka.ms/InstallAzureCLIDeb | bash

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WeatherForecast/WeatherForecast.csproj", "src/WeatherForecast/"]
RUN dotnet restore "src/WeatherForecast/WeatherForecast.csproj"
COPY . .
WORKDIR "/src/src/WeatherForecast"
RUN dotnet build "WeatherForecast.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherForecast.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherForecast.dll"]