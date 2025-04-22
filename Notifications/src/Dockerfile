FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY ["Notifications.WebUI/Notifications.WebUI.csproj", "Notifications.WebUI/"]
COPY ["Notifications.Infrastructure/Notifications.Infrastructure.csproj", "Notifications.Infrastructure/"]
COPY ["Notifications.Application/Notifications.Application.csproj", "Notifications.Application/"]
COPY ["Notifications.Domain/Notifications.Domain.csproj", "Notifications.Domain/"]
RUN dotnet restore "Notifications.WebUI/Notifications.WebUI.csproj"
COPY . ./
RUN dotnet publish "Notifications.WebUI/Notifications.WebUI.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://0.0.0.0:80  
ENTRYPOINT [ "dotnet", "Notifications.WebUI.dll" ]