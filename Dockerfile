FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY *.sln .
COPY MvcSunCity/*.csproj ./app/
RUN dotnet restore
COPY MvcSunCity/. ./app/

WORKDIR /source/app
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MvcSunCity.dll"]


