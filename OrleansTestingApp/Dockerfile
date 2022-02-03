#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OrleansTestingApp/OrleansTestingApp.csproj", "OrleansTestingApp/"]
RUN dotnet restore "OrleansTestingApp/OrleansTestingApp.csproj"
COPY . .
WORKDIR "/src/OrleansTestingApp"
RUN dotnet build "OrleansTestingApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrleansTestingApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrleansTestingApp.dll"]