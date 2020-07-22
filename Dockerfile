FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["aspnetcore-starter.csproj", "./"]
RUN dotnet restore "./aspnetcore-starter.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "aspnetcore-starter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "aspnetcore-starter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspnetcore-starter.dll"]
