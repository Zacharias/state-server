# state-server

## Building The Application

This application is designed to run as a standalone dotnet app (with the appropiate SDKs insatlled on the machine), or as a dockeried app to be ran as part of a larger solution.

#### Method 1. Docker

Easiest way to build/run this application is run `docker-compose up --build`. 

If you don't have compose (but have Docker somehow, which is getting more edge-casey by the version), you can run `docker build -t stateserver .` and `docker run stateserver:latest -p 8080:5000` to get a locally-running copy from there.

The compose copy includes an nginx frontend container, which is a nod towards "productionizing" the application.

#### Method 2. dotnet

Dotnet apis have an in-built http server for web code, mostly (but not entirely) for development. With the appropiate sdk installs, execute `dotnet run` from the repository root.

## Using The Application

For end-user convience, I added a nuget package that publishes and consumes a SwaggerDoc, which is a API explorer that allows for some nice automation (for example, getting Angular servies to consume the API for a frontend).

If you are running the application in docker, the url is http://localhost:8080/swagger, otherwise, it'd be on the direct http://localhot:5000/swagger link. Example `curl` commands are included.
