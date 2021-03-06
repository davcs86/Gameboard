# Gameboard
@davcs86's gameboard demo.

## Stack

### Back-end

- Asp.Net Core 1.0.1 (MVC + Web API)
- [NoDb](https://github.com/joeaudette/NoDb) for data persistence.

### Front-end

- Angular 1.5 (Angular-UI, UI-Grid, Angular-Schema-Form) with ES6,
- SASS (Bootstrap-sass),
- Webpack 2 as module bundler.
- Gulp as Task Runner

### Testing

- xUnit,
- Moq

## Online demo:

[http://gameboard-dcastillo.azurewebsites.net/#!/products/list](http://gameboard-dcastillo.azurewebsites.net/#!/products/list)

## Requirements:

- [DotNet Core SDK](https://www.microsoft.com/net/download/core#/sdk)

## Installation

```
1. Clone the repo
    $> git clone https://github.com/davcs86/Gameboard
2. Change directory to the repo
    $> cd Gameboard
3. Restore the Nuget packages
    $> dotnet restore **/project.json
4. Build the src projects
    $> dotnet build src/**/project.json
5. Install and build the front-end dependencies
    $> cd src/Gameboard
    $> npm install
    $> gulp default
    $> cd ..
6. Run the tests
   $> dotnet test test/Gameboard_Tests/project.json
```
