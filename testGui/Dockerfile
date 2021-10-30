FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-envy
WORKDIR /Web

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /Web
EXPOSE 1001
ENV ASPNETCORE_URLS=http://test:1001
COPY --from=build-envy /Web/out .
ENTRYPOINT ["dotnet", "testGui.dll"]