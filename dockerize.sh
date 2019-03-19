#!/bin/sh

dotnet publish -c Debug
docker build --build-arg Configuration=Debug -t check-yo-self:1.0.0 .