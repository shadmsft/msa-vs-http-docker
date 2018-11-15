FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY webappmvc/webappmvc.csproj webappmvc/
COPY models/models.csproj models/
RUN dotnet restore webappmvc/webappmvc.csproj
COPY . .
WORKDIR /src/webappmvc
RUN dotnet build webappmvc.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish webappmvc.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "webappmvc.dll"]
