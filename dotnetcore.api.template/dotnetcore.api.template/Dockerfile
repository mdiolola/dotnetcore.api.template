FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50447
EXPOSE 63944
EXPOSE 1433

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY dotnetcore.api.template/dotnetcore.api.template.csproj dotnetcore.api.template/
RUN dotnet restore dotnetcore.api.template/dotnetcore.api.template.csproj
COPY . .

WORKDIR /src/dotnetcore.api.template
RUN dotnet build dotnetcore.api.template.csproj -c Release -o /app
ENV ASPNETCORE_ENVIRONMENT local
RUN dotnet ef database update --project dotnetcore.api.template.csproj

FROM build AS publish
RUN dotnet publish dotnetcore.api.template.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "dotnetcore.api.template.dll"]

