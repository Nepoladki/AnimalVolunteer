#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AnimalVolunteer.API/AnimalVolunteer.API.csproj", "src/AnimalVolunteer.API/"]
COPY ["src/AnimalVolunteer.Application/AnimalVolunteer.Application.csproj", "src/AnimalVolunteer.Application/"]
COPY ["src/AnimalVolunteer.Domain/AnimalVolunteer.Domain.csproj", "src/AnimalVolunteer.Domain/"]
COPY ["src/AnimalVolunteer.Infrastructure/AnimalVolunteer.Infrastructure.csproj", "src/AnimalVolunteer.Infrastructure/"]
RUN dotnet restore "./src/AnimalVolunteer.API/AnimalVolunteer.API.csproj"
COPY . .
WORKDIR "/src/src/AnimalVolunteer.API"
RUN dotnet build "AnimalVolunteer.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AnimalVolunteer.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimalVolunteer.API.dll"]