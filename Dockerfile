#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RestApiProject.Api/RestApiProject.WebHost.csproj", "RestApiProject.Api/"]
COPY ["RestApiProject.Concrete/RestApiProject.BL.csproj", "RestApiProject.Concrete/"]
COPY ["RestApiProject.Contracts/RestApiProject.Contracts.csproj", "RestApiProject.Contracts/"]
COPY ["RestApiProject.Dto/RestApiProject.Dto.csproj", "RestApiProject.Dto/"]
COPY ["RestApiProject.Core/RestApiProject.Core.csproj", "RestApiProject.Core/"]
COPY ["RestApiProject.DAL/RestApiProject.DAL.csproj", "RestApiProject.DAL/"]
COPY ["RestApiProject.HttpApi/RestApiProject.HttpApi.csproj", "RestApiProject.HttpApi/"]
RUN dotnet restore "RestApiProject.Api/RestApiProject.WebHost.csproj"
COPY . .
WORKDIR "/src/RestApiProject.Api"
RUN dotnet build "RestApiProject.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestApiProject.WebHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestApiProject.WebHost.dll"]