# Check-Yo-Self

This Angular 5, .NET Core application allows a user to list all the dates
on which they will receive a paycheck in a specified number of years.
It also allows a user to query for the months in which a specified
number of checks will be received over the course of the next year.

## Prerequisites

* [Visual Studio Code](https://code.visualstudio.com/download)
* [Omnisharp Extension for Visual Studio Code (install within Visual Studio Code)](https://github.com/OmniSharp/omnisharp-vscode/blob/master/debugger.md)
* [.NET Core SDK 2.0](https://dotnet.microsoft.com/download/dotnet-core/2.0)
* [NodeJS (LTS)](https://nodejs.org/en/download/)
* Docker Desktop for Windows & Mac `OR` Docker CE for Linux:
    * [Windows or Mac](https://www.docker.com/products/docker-desktop)
    * [Linux](https://docs.docker.com/install/)

## Uses

This application is intended to be used as a training tool.
In order to package the application into a Docker container,
run the following command:

```bash
npm run publish:all:release
```

This command will run the following commands:

```bash
dotnet publish -c Release
docker build --build-arg Configuration=Release . -t paycheck-calculator-web
```

The resulting container will be tagged `paycheck-calculator-web`
