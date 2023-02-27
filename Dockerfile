FROM mcr.microsoft.com/dotnet/aspnet:7.0
ARG Configuration=Release
WORKDIR /app
EXPOSE 8080
COPY bin/$Configuration/net7.0/publish .
RUN useradd -ms /bin/bash armadillo
USER armadillo
ENV urls "http://*:8080"
ENTRYPOINT ["dotnet", "check-yo-self.dll"]