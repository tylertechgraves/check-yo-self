dotnet publish -c Release
docker build . -t check-yo-self:1.0.0 --build-arg Configuration=Release