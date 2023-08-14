FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/PharmaService.Migrator/PharmaService.Migrator.csproj", "src/PharmaService.Migrator/"]
RUN dotnet restore "src/PharmaService.Migrator/PharmaService.Migrator.csproj"
COPY . .
WORKDIR "/src/src/PharmaService.Migrator"
RUN dotnet build "PharmaService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PharmaService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PharmaService.Migrator.dll"]
