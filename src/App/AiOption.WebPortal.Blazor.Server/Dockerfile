FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/App/AiOption.WebPortal.Blazor.Server/AiOption.WebPortal.Blazor.Server.csproj", "src/App/AiOption.WebPortal.Blazor.Server/"]
COPY ["src/App/AiOption.WebPortal.Blazor.Client/AiOption.WebPortal.Blazor.Client.csproj", "src/App/AiOption.WebPortal.Blazor.Client/"]
COPY ["src/App/AiOption.WebPortal.Blazor.Shared/AiOption.WebPortal.Blazor.Shared.csproj", "src/App/AiOption.WebPortal.Blazor.Shared/"]
RUN dotnet restore "src/App/AiOption.WebPortal.Blazor.Server/AiOption.WebPortal.Blazor.Server.csproj"
COPY . .
WORKDIR "/src/src/App/AiOption.WebPortal.Blazor.Server"
RUN dotnet build "AiOption.WebPortal.Blazor.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AiOption.WebPortal.Blazor.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AiOption.WebPortal.Blazor.Server.dll"]