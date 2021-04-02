FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /src
COPY . ./

WORKDIR /src/Api
RUN dotnet restore 
RUN dotnet publish -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY --from=build-env /src/Api/out .
ENTRYPOINT ["dotnet", "Api.dll"]

RUN apt-get update && apt-get install -y curl
HEALTHCHECK CMD curl --fail http://localhost:80/health

EXPOSE 80