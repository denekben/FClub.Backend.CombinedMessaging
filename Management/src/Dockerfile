FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY ["Management.WebUI/Management.WebUI.csproj", "Management.WebUI/"]
COPY ["Management.Infrastructure/Management.Infrastructure.csproj", "Management.Infrastructure/"]
COPY ["Management.Application/Management.Application.csproj", "Management.Application/"]
COPY ["Management.Domain/Management.Domain.csproj", "Management.Domain/"]
COPY ["Management.Shared/Management.Shared.csproj", "Management.Shared/"]
RUN dotnet restore "Management.WebUI/Management.WebUI.csproj"
COPY . ./
RUN dotnet publish "Management.WebUI/Management.WebUI.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://0.0.0.0:80  
ENTRYPOINT [ "dotnet", "Management.WebUI.dll" ]