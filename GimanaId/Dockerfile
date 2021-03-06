#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update
RUN apt-get install -y npm
RUN curl -sL https://deb.nodesource.com/setup_12.x |  bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
RUN apt-get update
RUN apt-get install -y npm
RUN curl -sL https://deb.nodesource.com/setup_12.x |  bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["GimanaId/GimanaId.csproj", "GimanaId/"]
RUN dotnet restore "GimanaId/GimanaId.csproj"
COPY . .
WORKDIR "/src/GimanaId"
RUN dotnet build "GimanaId.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GimanaId.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GimanaId.dll"]