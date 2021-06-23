#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001
EXPOSE 44365

# Install nodejs
#RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
#RUN apt-get install -y nodejs

RUN apt update && apt -y upgrade
RUN apt-get install -y wget

RUN apt-get install -y gnupg
RUN apt-get install -y gnupg1
RUN apt-get install -y gnupg2

RUN wget  https://deb.nodesource.com/setup_12.x
RUN echo 'deb https://deb.nodesource.com/node_12.x  stretch  main' > /etc/apt/sources.list.d/nodesource.list
RUN wget -qO - https://deb.nodesource.com/gpgkey/nodesource.gpg.key | apt-key add -

RUN apt update
RUN apt install -y nodejs 
#RUN npm install -g @angular/cli 


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build

WORKDIR /src/
COPY [".", "/src"]

# install npm
WORKDIR /src/FoodApp/food-app/

RUN apt update && apt -y upgrade
RUN apt-get install -y wget

RUN apt-get install -y gnupg
RUN apt-get install -y gnupg1
RUN apt-get install -y gnupg2

RUN wget  https://deb.nodesource.com/setup_12.x
RUN echo 'deb https://deb.nodesource.com/node_12.x  stretch  main' > /etc/apt/sources.list.d/nodesource.list
RUN wget -qO - https://deb.nodesource.com/gpgkey/nodesource.gpg.key | apt-key add -

RUN apt update
RUN apt install -y nodejs 

WORKDIR /src/FoodApp/food-app
RUN npm install react-router-dom
RUN npm install @types/react-router-dom

# finish npm and package install

WORKDIR /src/

RUN dotnet build  -c Release -o /app/build

RUN dotnet publish -c Release -o /app/publish

RUN ls -ltr ./food-system.yaml

COPY ["./food-system.yaml", "/app/publish/food-system.yaml"]

RUN ls /app/publish

WORKDIR /src/FoodApp/food-app

RUN echo "start of npm run build"
RUN npm run build
RUN echo "end of npm run build"

RUN mkdir /app/publish/ClientApp
RUN cp -R /src/FoodApp/food-app/build/* /app/publish/ClientApp

FROM base AS publish


EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001
EXPOSE 44365
EXPOSE 5500
EXPOSE 5501

ENV ASPNETCORE_ENVIRONMENT Production
#ENV ASPNETCORE_URLS http://*:5000;https://*:5001
ENV ASPNETCORE_URLS http://*:5000

WORKDIR /app
COPY --from=build /app/publish .
#RUN cp -R ClientApp/build/* .
#RUN cp -R ClientApp/build/* ClientApp
#RUN dotnet dev-certs https
ENTRYPOINT ["dotnet", "FoodApp.dll"]



