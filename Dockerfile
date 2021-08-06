FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ARG Configuration=Release
WORKDIR /app
EXPOSE 8080
COPY bin/$Configuration/netcoreapp3.1/publish .
RUN useradd -ms /bin/bash armadillo
USER armadillo
ENV urls "http://*:8080"
ENTRYPOINT ["dotnet", "check-yo-self.dll"]