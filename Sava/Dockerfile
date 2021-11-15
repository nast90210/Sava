FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Sava/Sava.csproj", "Sava/"]
RUN dotnet restore "Sava/Sava.csproj"
COPY . .
WORKDIR "/src/Sava"
RUN dotnet build "Sava.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sava.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sava.dll"]
