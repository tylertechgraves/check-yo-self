FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ARG Configuration=Debug
WORKDIR /app
EXPOSE 80
COPY bin/$Configuration/netcoreapp3.1/publish .
ENV urls "http://*:80"
ENTRYPOINT ["dotnet", "paycheck_calculator_web.dll"]