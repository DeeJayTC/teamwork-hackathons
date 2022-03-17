#FROM microsoft/aspnetcore:2.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:2.0 AS build
#WORKDIR /src
#COPY TeamworkHackathon.csproj TeamworkHackathon.csproj
#RUN dotnet restore /src/TeamworkHackathon.csproj
#WORKDIR /src/TeamworkCSW
#COPY . .
#RUN dotnet build TeamworkHackathon.csproj -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish TeamworkHackathon.csproj -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "TeamworkHackathon.dll"]
#

#FROM microsoft/aspnetcore:2.0
#ARG source
#WORKDIR /app
#EXPOSE 9118
#COPY ${source:-obj/Docker/publish} .
#ENTRYPOINT ["dotnet", "TeamworkCSW.dll"]
#
FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY teamworkhackathon.csproj TeamworkHackathon/teamworkhackathon.csproj
RUN dotnet restore /src/TeamworkHackathon/teamworkhackathon.csproj
WORKDIR /src/TeamworkHackathon
COPY . .

RUN dotnet install dotnet-watch -g

ENTRYPOINT dotnet watch run --no-restore
