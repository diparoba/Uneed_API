FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY ./publish /app

ENV ASPNETCORE_URLS=http://*:6000
ENV ASPNETCORE_ENVIRONMENT docker

EXPOSE 6000
ENTRYPOINT ["dotnet", "/app/Uneed_Ocelot.dll"]