#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/services/NSE.Identidade.Api/NSE.Identidade.Api.csproj", "src/services/NSE.Identidade.Api/"]
RUN dotnet restore "src/services/NSE.Identidade.Api/NSE.Identidade.Api.csproj"
COPY . .
WORKDIR "/src/src/services/NSE.Identidade.Api"
RUN dotnet build "NSE.Identidade.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Identidade.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Identidade.Api.dll"]