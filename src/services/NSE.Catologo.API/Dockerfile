#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/services/NSE.Catologo.API/NSE.Catologo.API.csproj", "src/services/NSE.Catologo.API/"]
RUN dotnet restore "src/services/NSE.Catologo.API/NSE.Catologo.API.csproj"
COPY . .
WORKDIR "/src/src/services/NSE.Catologo.API"
RUN dotnet build "NSE.Catologo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Catologo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Catologo.API.dll"]