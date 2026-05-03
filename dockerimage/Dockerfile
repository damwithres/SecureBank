FROM mcr.microsoft.com/dotnet/sdk:8.0 AS securebank
WORKDIR /app

COPY src/SecureBank/SecureBank.csproj ./
RUN dotnet restore;

COPY src/SecureBank/ ./

RUN dotnet publish --output /app/out --configuration Release --runtime linux-x64 -p:EnvironmentName=Production;

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS storeapi
WORKDIR /app

COPY src/StoreAPI/StoreAPI.csproj ./
RUN dotnet restore

COPY src/StoreAPI/ ./

RUN dotnet publish --output /app/out --configuration Release --runtime linux-x64 -p:EnvironmentName=Production;

FROM mcr.microsoft.com/mssql/server:2022-latest

# Modified by Rezilant AI, 2026-05-01 18:35:33 GMT, Adding non-root user for principle of least privilege
# Create non-root user and prepare to switch after root operations complete
RUN useradd -m -u 1001 appuser

USER root

COPY dockerimage/entrypoint.sh ./entrypoint.sh
RUN chmod o+x ./entrypoint.sh

COPY dockerimage/scripts ./scripts
RUN chmod +x -R scripts

COPY --from=securebank /app/out ./SecureBank
RUN mkdir /SecureBank/Ctf

COPY --from=storeapi /app/out ./StoreAPI
    
COPY --from=maildev/maildev:1.1.0 /usr/src/app ./maildev

EXPOSE 80
EXPOSE 5000

EXPOSE 1025
EXPOSE 1080

EXPOSE 1433

# Modified by Rezilant AI, 2026-05-01 18:35:33 GMT, Switching to non-root user to reduce attack surface
# Switch to non-privileged user after completing root-level operations
USER appuser

ENTRYPOINT ["./entrypoint.sh"]