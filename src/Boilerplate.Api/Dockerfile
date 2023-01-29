##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG SOURCE_SECRETS_PATH=${SOURCE_SECRETS_PATH} \
    DESTINATION_SECRETS_PATH=${DESTINATION_SECRETS_PATH} \
    JIBAN_Initial=${JIBAN__Initial} \
    USER_SECRETS_ID=${USER_SECRETS_ID} \
    ConnectionStrings__SqlServerConnection=${ConnectionStrings__SqlServerConnection} \
    AMBIENTE=${AMBIENTE}

#COMANDOS PARA SETEAR EL PROPIETARIO DEL DIRECTORIO
RUN echo "SOURCE_SECRETS_PATH: ${SOURCE_SECRETS_PATH}"    
RUN echo "DESTINATION_SECRETS_PATH: ${DESTINATION_SECRETS_PATH}"
RUN echo "JIBAN_Initial: ${JIBAN__Initial}"
RUN echo "USER_SECRETS_ID: ${USER_SECRETS_ID}"
RUN echo "ConnectionStrings__SqlServerConnection: ${ConnectionStrings__SqlServerConnection}"

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/src/Boilerplate.Api"

FROM build AS publish
RUN dotnet publish "Boilerplate.Api.csproj" -c Release -o /app/publish

FROM base AS final
#INSTALAR LIBRERIAS UBUNTU
RUN apt-get update && apt-get install -y apt-utils iputils-ping telnet vim
WORKDIR /app
#COPY ["${SOURCE_SECRETS_PATH}", "${DESTINATION_SECRETS_PATH}"]
COPY ["./Certs", "/src/Certs"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boilerplate.Api.dll"]