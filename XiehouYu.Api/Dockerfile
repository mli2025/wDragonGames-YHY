FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["XiehouYu.Api/XiehouYu.Api.csproj", "XiehouYu.Api/"]
RUN dotnet restore "XiehouYu.Api/XiehouYu.Api.csproj"
COPY . .
WORKDIR "/src/XiehouYu.Api"
RUN dotnet build "XiehouYu.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XiehouYu.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XiehouYu.Api.dll"] 