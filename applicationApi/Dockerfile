FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-envy
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /App
EXPOSE 44354
ENV ASPNETCORE_URLS=http://+:44354
COPY --from=build-envy /App/out .
ENTRYPOINT ["dotnet", "applicationApi.dll"]