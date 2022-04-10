#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebStore/WebStore.csproj", "WebStore/"]
COPY ["WebStore.Domain/WebStore.Domain.csproj", "WebStore.Domain/"]
COPY ["WebStore.DAL/WebStore.DAL.csproj", "WebStore.DAL/"]
RUN dotnet restore "WebStore/WebStore.csproj"
COPY . .
WORKDIR "/src/WebStore"
RUN dotnet build "WebStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStore.dll"]
