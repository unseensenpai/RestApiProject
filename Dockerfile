#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/RestApiProject.Api/RestApiProject.WebHost.csproj", "src/RestApiProject.Api/"]
COPY ["src/RestApiProject.Concrete/RestApiProject.BL.csproj", "src/RestApiProject.Concrete/"]
COPY ["src/RestApiProject.Contracts/RestApiProject.Contracts.csproj", "src/RestApiProject.Contracts/"]
COPY ["src/RestApiProject.Dto/RestApiProject.Dto.csproj", "src/RestApiProject.Dto/"]
COPY ["src/RestApiProject.Core/RestApiProject.Core.csproj", "src/RestApiProject.Core/"]
COPY ["src/RestApiProject.DAL/RestApiProject.DAL.csproj", "src/RestApiProject.DAL/"]
COPY ["src/RestApiProject.HttpApi/RestApiProject.HttpApi.csproj", "src/RestApiProject.HttpApi/"]
RUN dotnet restore "src/RestApiProject.Api/RestApiProject.WebHost.csproj"
COPY . .
WORKDIR "/src/src/RestApiProject.Api"
RUN dotnet build "RestApiProject.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestApiProject.WebHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestApiProject.WebHost.dll"]