FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/PersonalityTest.Api/PersonalityTest.Api.csproj", "PersonalityTest.Api/"]
COPY ["src/PersonalityTest.Core/PersonalityTest.Core.csproj", "PersonalityTest.Core/"]
COPY ["src/PersonalityTest.Shared/PersonalityTest.Shared.csproj", "PersonalityTest.Shared/"]
COPY ["src/PersonalityTest.Data/PersonalityTest.Data.csproj", "PersonalityTest.Data/"]
RUN dotnet restore "PersonalityTest.Shared/PersonalityTest.Shared.csproj"
RUN dotnet restore "PersonalityTest.Data/PersonalityTest.Data.csproj"
RUN dotnet restore "PersonalityTest.Core/PersonalityTest.Core.csproj"
RUN dotnet restore "PersonalityTest.Api/PersonalityTest.Api.csproj"
COPY ./src .
WORKDIR "/src/PersonalityTest.Api"
RUN dotnet build "PersonalityTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalityTest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalityTest.Api.dll"]
