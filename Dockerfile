FROM microsoft/aspnetcore:2.0
ARG Configuration=Debug
WORKDIR /app
EXPOSE 80
COPY bin/$Configuration/netcoreapp2.0/publish .
ENV urls "http://*:80"
ENTRYPOINT ["dotnet", "paycheck_calculator_web.dll"]