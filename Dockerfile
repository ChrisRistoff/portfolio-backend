FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY ["portfolio/portfolio.csproj", "portfolio/"]
RUN dotnet restore "portfolio/portfolio.csproj"

COPY portfolio/ ./portfolio/
WORKDIR /app/OpenSourceRecipe
RUN dotnet publish "portfolio.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/portfolio/out .

ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "portfolio.dll"]