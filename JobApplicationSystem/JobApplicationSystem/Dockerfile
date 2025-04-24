#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["JobApplicationSystem/JobApplicationSystem.csproj", "JobApplicationSystem/"]
RUN dotnet restore "JobApplicationSystem/JobApplicationSystem.csproj"
COPY . .
WORKDIR "/src/JobApplicationSystem"
RUN dotnet build "JobApplicationSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobApplicationSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobApplicationSystem.dll"]