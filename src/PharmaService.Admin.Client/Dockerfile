FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/PharmaService.Admin.Client/PharmaService.Admin.Client.csproj", "src/PharmaService.Admin.Client/"]
COPY ["src/PharmaService.IntegrationClient.Http/PharmaService.IntegrationClient.Http.csproj", "src/PharmaService.IntegrationClient.Http/"]
RUN dotnet restore "src/PharmaService.Admin.Client/PharmaService.Admin.Client.csproj"
COPY . .
WORKDIR "/src/src/PharmaService.Admin.Client"
RUN dotnet build "PharmaService.Admin.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PharmaService.Admin.Client.csproj" -c Release -o /app/publish
FROM nginx:alpine
WORKDIR /var/www/web
ENV NGINX_ENVSUBST_OUTPUT_DIR=/var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY src/PharmaService.Admin.Client/nginx.conf /etc/nginx/nginx.conf
COPY src/PharmaService.Admin.Client/appsettings.json.template /etc/nginx/templates/

EXPOSE 80