FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

ADD IP_MVC ./IP_MVC
ADD BL ./BL 
ADD DAL ./DAL
ADD Domain ./Domain
RUN dotnet restore IP_MVC/IP_MVC.csproj

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "IP_MVC.dll"]