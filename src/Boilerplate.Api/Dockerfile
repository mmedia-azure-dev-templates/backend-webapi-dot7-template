##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG SOURCE_SECRETS_PATH=${SOURCE_SECRETS_PATH} \
    DESTINATION_SECRETS_PATH=${DESTINATION_SECRETS_PATH} \
    CUSTOMUID=${AMBIENTE} \
    CUSTOMGID=${AMBIENTE} \
    APACHE=${AMBIENTE}

#COMANDOS PARA SETEAR EL PROPIETARIO DEL DIRECTORIO
RUN echo "SOURCE_SECRETS_PATH: ${SOURCE_SECRETS_PATH}"    
RUN echo "DESTINATION_SECRETS_PATH: ${DESTINATION_SECRETS_PATH}"    
RUN echo "CUSTOMGID: ${SA_PASSWORD}"
RUN echo "APACHE: ${SA_PASSWORD}"

# Install cultures (same approach as Alpine SDK image)
#RUN apk add --no-cache icu-libs

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["${SOURCE_SECRETS_PATH}", "${DESTINATION_SECRETS_PATH}"]
COPY ["./Directory.Packages.props", "./"]
COPY ["src/Boilerplate.Api/Boilerplate.Api.csproj", "src/Boilerplate.Api/"]
COPY ["src/Boilerplate.Application/Boilerplate.Application.csproj", "src/Boilerplate.Application/"]
COPY ["src/Boilerplate.Domain/Boilerplate.Domain.csproj", "src/Boilerplate.Domain/"]
COPY ["src/Boilerplate.Infrastructure/Boilerplate.Infrastructure.csproj", "src/Boilerplate.Infrastructure/"]
COPY . .
WORKDIR "/src/src/Boilerplate.Api"

FROM build AS publish
RUN dotnet publish "Boilerplate.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boilerplate.Api.dll"]