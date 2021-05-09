FROM microsoft/dotnet:2.2.104-sdk-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY src/*.csproj .
RUN dotnet restore

# copy everything else and build app
COPY src/. .
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80

ENTRYPOINT ["dotnet", "whoami.dll"]
