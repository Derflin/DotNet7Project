FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-envy
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /App
EXPOSE 17588
ENV ASPNETCORE_URLS=http://+:17588
COPY --from=build-envy /App/out .
ENTRYPOINT ["dotnet", "applicationGenSensorData.dll"]