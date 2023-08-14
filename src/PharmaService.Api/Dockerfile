FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/PharmaService.Api/PharmaService.Api.csproj", "src/PharmaService.Api/"]
COPY ["src/PharmaService.DataAccess.PostgresSql/PharmaService.DataAccess.PostgresSql.csproj", "src/PharmaService.DataAccess.PostgresSql/"]
COPY ["src/PharmaService.DataAccess/PharmaService.DataAccess.csproj", "src/PharmaService.DataAccess/"]
COPY ["src/PharmaService.Domain/PharmaService.Domain.csproj", "src/PharmaService.Domain/"]
COPY ["src/PharmaService.Service/PharmaService.Service.csproj", "src/PharmaService.Service/"]
RUN dotnet restore "src/PharmaService.Api/PharmaService.Api.csproj"
COPY . .
WORKDIR "/src/src/PharmaService.Api"
RUN dotnet build "PharmaService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PharmaService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PharmaService.Api.dll"]
