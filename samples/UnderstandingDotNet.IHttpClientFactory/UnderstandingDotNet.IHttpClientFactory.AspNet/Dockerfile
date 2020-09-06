#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["UnderstandingDotNet.IHttpClientFactory.AspNet/UnderstandingDotNet.IHttpClientFactory.AspNet.csproj", "UnderstandingDotNet.IHttpClientFactory.AspNet/"]
RUN dotnet restore "UnderstandingDotNet.IHttpClientFactory.AspNet/UnderstandingDotNet.IHttpClientFactory.AspNet.csproj"
COPY . .
WORKDIR "/src/UnderstandingDotNet.IHttpClientFactory.AspNet"
RUN dotnet build "UnderstandingDotNet.IHttpClientFactory.AspNet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnderstandingDotNet.IHttpClientFactory.AspNet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnderstandingDotNet.IHttpClientFactory.AspNet.dll"]