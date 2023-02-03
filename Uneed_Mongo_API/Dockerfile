FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY ./publish /app

ENV ASPNETCORE_URLS=http://*:5002
ENV ASPNETCORE_ENVIRONMENT docker

EXPOSE 5002
ENTRYPOINT ["dotnet", "/app/Uneed_Mongo_API.dll"]