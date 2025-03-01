FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY TravelAgency.sln ./
COPY src/TravelAgency.API/TravelAgency.API.csproj src/TravelAgency.API/
COPY src/TravelAgency.Application/TravelAgency.Application.csproj src/TravelAgency.Application/
COPY src/TravelAgency.Infrastructure/TravelAgency.Infrastructure.csproj src/TravelAgency.Infrastructure/
COPY src/TravelAgency.Domain/TravelAgency.Domain.csproj src/TravelAgency.Domain/

RUN dotnet restore src/TravelAgency.API/TravelAgency.API.csproj

COPY . .  
RUN dotnet publish src/TravelAgency.API/TravelAgency.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "TravelAgency.API.dll"]
