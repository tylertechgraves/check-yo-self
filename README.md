# Check-Yo-Self

This Angular 5, .NET Core application allows a user to list all the dates
on which they will receive a paycheck in a specified number of years.
It also allows a user to query for the months in which a specified
number of checks will be received over the course of the next year.

## Prerequisites

You must be running Windows 10 Pro, MacOS, or Linux to in order to install Docker Desktop.
If you don't have Docker Desktop installed, you will be unable to build or run
Docker containers, making this exercise void of value.

* [Visual Studio Code](https://code.visualstudio.com/download)
* [Omnisharp Extension for Visual Studio Code (install within Visual Studio Code)](https://github.com/OmniSharp/omnisharp-vscode/blob/master/debugger.md)
* [.NET Core SDK (take the latest version ... probably 2.2)](https://dotnet.microsoft.com/download)
* [NodeJS (LTS)](https://nodejs.org/en/download/)
* Docker Desktop for Windows & Mac `OR` Docker CE for Linux:
    * [Windows or Mac](https://www.docker.com/products/docker-desktop)
    * [Linux](https://docs.docker.com/install/)

## Uses

This application is intended to be used as a training tool.
In order to package the application into a Docker container,
run the following commands:

```bash
dotnet publish -c Release
docker build --build-arg Configuration=Release -t check-yo-self:1.0.0 .
```

The resulting container will be tagged `check-yo-self:1.0.0`
