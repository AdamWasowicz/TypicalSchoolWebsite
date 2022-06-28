FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


ARG ASPNETCORE_EXPOSE_PORT ASPNETCORE_EXPOSE_PORT
EXPOSE $ASPNETCORE_EXPOSE_PORT


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TypicalSchoolWebsite_API.csproj", "."]
RUN dotnet restore "./TypicalSchoolWebsite_API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TypicalSchoolWebsite_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TypicalSchoolWebsite_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TypicalSchoolWebsite_API.dll"]