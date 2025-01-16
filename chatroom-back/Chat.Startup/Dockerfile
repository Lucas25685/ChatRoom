FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ChatRoom.Startup/ChatRoom.Startup.csproj", "ChatRoom.Startup/"]
COPY ["ChatRoom.Repository/ChatRoom.Repository.csproj", "ChatRoom.Repository/"]
COPY ["ChatRoom.ApiModel/ChatRoom.ApiModel.csproj", "ChatRoom.ApiModel/"]
COPY ["ChatRoom.Api/ChatRoom.Api.csproj", "ChatRoom.Api/"]
COPY ["ChatRoom.Business/ChatRoom.Business.csproj", "ChatRoom.Business/"]
COPY ["ChatRoom.CommonModel/ChatRoom.CommonModel.csproj", "ChatRoom.CommonModel/"]
COPY ["ChatRoom.Model/ChatRoom.Model.csproj", "ChatRoom.Model/"]
COPY ["MedHubCompany.Extensions.Vidal/MedHubCompany.Extensions.Vidal.csproj", "MedHubCompany.Extensions.Vidal/"]
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["version.json", "version.json"]
COPY ["ChatRoom.sln", "ChatRoom.sln"]
RUN dotnet restore "ChatRoom.Startup/ChatRoom.Startup.csproj"
COPY . .
WORKDIR "/src/ChatRoom.Startup"
RUN dotnet build "ChatRoom.Startup.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChatRoom.Startup.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatRoom.Startup.dll"]
