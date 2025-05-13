FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY ["Logging.WebUI/Logging.WebUI.csproj", "Logging.WebUI/"]
COPY ["Logging.Infrastructure/Logging.Infrastructure.csproj", "Logging.Infrastructure/"]
COPY ["Logging.Application/Logging.Application.csproj", "Logging.Application/"]
COPY ["Logging.Domain/Logging.Domain.csproj", "Logging.Domain/"]
RUN dotnet restore "Logging.WebUI/Logging.WebUI.csproj"
COPY . ./
RUN dotnet publish "Logging.WebUI/Logging.WebUI.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://0.0.0.0:80  
ENTRYPOINT [ "dotnet", "Logging.WebUI.dll" ]