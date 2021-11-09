

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build


WORKDIR /app

COPY *.sln ./
COPY BusinessLogic/*.csproj BusinessLogic/
COPY DataAccessLogic/*.csproj DataAccessLogic/
COPY Models/*.csproj Models/
COPY UnitTests/*.csproj UnitTests/
COPY WebInterface/*.csproj WebInterface/
COPY Toolbox/*.csproj Toolbox/

RUN cd WebInterface && dotnet restore

COPY . ./
CMD /bin/bash
RUN dotnet publish -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime

WORKDIR /app
COPY --from=build /app/publish/ ./

CMD [ "dotnet", "WebInterface.dll" ]
