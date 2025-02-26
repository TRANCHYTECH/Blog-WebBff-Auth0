ARG BUILD_IMAGE=mcr.microsoft.com/dotnet/sdk:8.0-noble

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM $BUILD_IMAGE AS prepare-restore-files
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global --no-cache dotnet-subset
WORKDIR /src
COPY . .
RUN dotnet subset restore Tranchy.sln --root-directory /src --output /restore_subset

FROM $BUILD_IMAGE AS restore
WORKDIR /src
COPY ./dotnet-tools.json .
RUN dotnet tool restore
COPY --from=prepare-restore-files /restore_subset .
RUN dotnet restore

FROM restore AS build
ARG BUILD_CONFIGURATION=Release
COPY . .
WORKDIR "/src/Tranchy.Ask.Api"
RUN dotnet build "./Tranchy.Ask.Api.csproj" -c $BUILD_CONFIGURATION --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tranchy.Ask.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-build --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tranchy.Ask.Api.dll"]