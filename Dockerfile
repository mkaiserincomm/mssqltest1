FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine3.14 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.14 AS build
ARG SUFFIX
WORKDIR /src
COPY ["mssqltest1.csproj", "./"]
RUN dotnet restore "mssqltest1.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "mssqltest1.csproj" -c Release -o /app/build --version-suffix ${SUFFIX}

FROM build AS publish
ARG SUFFIX
RUN dotnet publish "mssqltest1.csproj" -c Release -o /app/publish --version-suffix ${SUFFIX}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mssqltest1.dll"]
