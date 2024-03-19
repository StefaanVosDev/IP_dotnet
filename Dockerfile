FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

ADD IP_MVC ./IP_MVC
ADD BL ./BL 
ADD DAL ./DAL
ADD Domain ./Domain
RUN apt-get update
RUN apt-get install npm nodejs -y
RUN dotnet restore IP_MVC/IP_MVC.csproj

RUN npm install --save-dev mini-css-extract-plugin @popperjs/core bootstrap jquery jquery-validation jquery-validation-unobtrusive

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "IP_MVC.dll"]