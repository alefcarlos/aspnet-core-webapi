FROM microsoft/dotnet:2.2-sdk AS build-env

RUN apt-get update -yqq > /dev/null && \
    apt-get install -yqq default-jre > /dev/null  
    
RUN dotnet tool install -g dotnet-sonarscanner

ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /app
COPY . ./
WORKDIR /app/

RUN dotnet restore --no-cache

RUN dotnet sonarscanner begin /k:"Template" /d:sonar.host.url="http://10.11.4.172:9000" /d:sonar.login="af909261a2f77dcd104c0431238a50c37a2a55e6" /d:sonar.cs.opencover.reportsPaths="./**/coverage.opencover.xml"
RUN dotnet build
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --no-build

RUN dotnet sonarscanner end /d:sonar.login="af909261a2f77dcd104c0431238a50c37a2a55e6"
