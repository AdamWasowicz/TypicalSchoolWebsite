FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


ARG ASPNETCORE_EXPOSE_PORT ASPNETCORE_EXPOSE_PORT
EXPOSE $ASPNETCORE_EXPOSE_PORT


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TypicalSchoolWebsite_FileStorageService.csproj", "."]
RUN dotnet restore "./TypicalSchoolWebsite_FileStorageService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TypicalSchoolWebsite_FileStorageService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TypicalSchoolWebsite_FileStorageService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TypicalSchoolWebsite_FileStorageService.dll"]