# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build-env /app/publish /app
EXPOSE 80
ENTRYPOINT ["dotnet", "/app/AgileProject.dll"]