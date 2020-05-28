#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LoggingReference.Web/LoggingReference.Web.csproj", "LoggingReference.Web/"]
RUN dotnet restore "LoggingReference.Web/LoggingReference.Web.csproj"
COPY . .
WORKDIR "/src/LoggingReference.Web"
RUN dotnet build "LoggingReference.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoggingReference.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoggingReference.Web.dll"]