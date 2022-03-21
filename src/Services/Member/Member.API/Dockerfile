FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app
COPY . ./

WORKDIR /app/src/Services/Member/Member.API

RUN dotnet restore
RUN dotnet publish --no-restore -c Release -o /app/out/

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "IMBox.Member.API.dll"]