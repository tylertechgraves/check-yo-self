# Check-Yo-Self

This Angular 10, .NET Core application allows a user to list all the dates
on which they will receive a paycheck in a specified number of years.
It also allows a user to query for the months in which a specified
number of checks will be received over the course of the next year.

A new module has also been added that allows a user to create, update,
and search for employees in a MySql database that is backed by
Elasticsearch for the querying of employees.

## Prerequisites

Instructions for preparing your machine for the check-yo-self app
are located in the check-yo-self-prep-script repo located at the following
url: <https://github.com/tylertechgraves/check-yo-self-prep-script>

## Building the check-yo-self container

The `dockerize.ps1` and `dockerize.sh` scripts located at the root of this project will build
the required Docker container for this service.  Just choose one to run,
based on the OS you're running.

## Accessing the check-yo-self frontend app

Once the check-yo-self Docker container is listening on port 80,
you can navigate to the app in a browser by going to the following URL:

[http://localhost:5000](http://localhost:5000)

## Debugging this Project

To debug this project, you must first start the Angular server. This is easy!
Just open a shell in your VSCode terminal, change your directory to the ClientApp
folder.  Then run `npm start`.

Once the Angular server is running, simply start the VSCode debugger, either by
going to the debug tab and clicking the run button or by hitting the F5 key.