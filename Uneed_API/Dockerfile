FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY ./publish /app

ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_ENVIRONMENT docker

EXPOSE 8080
ENTRYPOINT ["dotnet", "/app/Uneed_API.dll"]