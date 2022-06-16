FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["PersonalityTest.Api/PersonalityTest.Api.csproj", "PersonalityTest.Api/"]
RUN dotnet restore "PersonalityTest.Api/PersonalityTest.Api.csproj"
COPY . .
WORKDIR "/src/PersonalityTest.Api"
RUN dotnet build "PersonalityTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalityTest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalityTest.Api.dll"]
