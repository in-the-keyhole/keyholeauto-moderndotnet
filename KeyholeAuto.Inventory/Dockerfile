FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /app
COPY ./KeyholeAuto.Inventory/ .
RUN dotnet restore
RUN dotnet publish -c Release -o published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/KeyholeAuto.Inventory.API.dll" ]