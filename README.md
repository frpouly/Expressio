# Expressio

Generate mixed expressions, remake of [ExpGen](https://github.com/Gulantib/ExpGen)

## Run the App

### Using Dotnet CLI

```dotnetcli
cd src/Expressio
dotnet run
```

Default route for the app is [http://localhost:5289/](http://localhost:5289/).

In development environment, a swagger for the API is also accessible through [http://localhost:5289/swagger/index.html](http://localhost:5289/swagger/index.html).

### Using Docker

```shell
docker build -t expressio -f Dockerfile ./src/Expressio
docker run -d -p 5289:8080 expressio 
```

Default route for the app is [http://localhost:5289/](http://localhost:5289/).

## Run the test suite

```dotnetcli
cd src
dotnet test
```
