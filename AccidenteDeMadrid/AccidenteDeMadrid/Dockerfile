# ============================================================
# Dockerfile multi-etapa: Práctica Accidente De Madrid
# ============================================================

# ETAPA 1: Build + Publish
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY AccidenteDeMadrid/AccidenteDeMadrid.csproj ./AccidenteDeMadrid/
RUN dotnet restore AccidenteDeMadrid/AccidenteDeMadrid.csproj

COPY . .

RUN dotnet publish AccidenteDeMadrid/AccidenteDeMadrid.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

# ETAPA 2: Runtime (imagen final ligera)
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Data/*.csv ya viene dentro de /app/Data gracias al csproj
ENTRYPOINT ["dotnet", "AccidenteDeMadrid.dll"]