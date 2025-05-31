FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Chatter.Api/Chatter.Api.csproj", "src/Chatter.Api/"]
COPY ["src/Chatter.Application/Chatter.Application.csproj", "src/Chatter.Application/"]
COPY ["src/Chatter.Domain/Chatter.Domain.csproj", "src/Chatter.Domain/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
COPY ["src/Chatter.Infrastructure/Chatter.Infrastructure.csproj", "src/Chatter.Infrastructure/"]
RUN dotnet restore "src/Chatter.Api/Chatter.Api.csproj"
COPY . .
WORKDIR "/src/src/Chatter.Api"
RUN dotnet build "Chatter.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Chatter.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chatter.Api.dll"]
