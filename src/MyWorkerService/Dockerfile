#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/MyWorkerService/MyWorkerService.csproj", "src/MyWorkerService/"]
RUN dotnet restore "src/MyWorkerService/MyWorkerService.csproj"
COPY . .
WORKDIR "/src/src/MyWorkerService"
RUN dotnet build "MyWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyWorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyWorkerService.dll"]