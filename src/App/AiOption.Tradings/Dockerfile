FROM microsoft/dotnet:2.2-runtime-stretch-slim AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY src/App/AiOption.Tradings/AiOption.Tradings.csproj src/App/AiOption.Tradings/
COPY src/AiOption.Infrastructure.ReadStores/AiOption.Infrastructure.ReadStores.csproj src/AiOption.Infrastructure.ReadStores/
COPY src/AiOption/AiOption.csproj src/AiOption/
COPY src/AiOption.Infrastructure/AiOption.Infrastructure.csproj src/AiOption.Infrastructure/
COPY src/AiOption.Application/AiOption.Application.csproj src/AiOption.Application/
COPY src/AiOption.Infrastructure.EventStores/AiOption.Infrastructure.EventStores.csproj src/AiOption.Infrastructure.EventStores/
RUN dotnet restore src/App/AiOption.Tradings/AiOption.Tradings.csproj
COPY . .
WORKDIR /src/src/App/AiOption.Tradings
RUN dotnet build AiOption.Tradings.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AiOption.Tradings.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AiOption.Tradings.dll"]
