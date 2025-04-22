FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY ["AccessControl.WebUI/AccessControl.WebUI.csproj", "AccessControl.WebUI/"]
COPY ["AccessControl.Infrastructure/AccessControl.Infrastructure.csproj", "AccessControl.Infrastructure/"]
COPY ["AccessControl.Application/AccessControl.Application.csproj", "AccessControl.Application/"]
COPY ["AccessControl.Domain/AccessControl.Domain.csproj", "AccessControl.Domain/"]
COPY ["AccessControl.Shared/AccessControl.Shared.csproj", "AccessControl.Shared/"]
RUN dotnet restore "AccessControl.WebUI/AccessControl.WebUI.csproj"
COPY . ./
RUN dotnet publish "AccessControl.WebUI/AccessControl.WebUI.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://0.0.0.0:80  
ENTRYPOINT [ "dotnet", "AccessControl.WebUI.dll" ]