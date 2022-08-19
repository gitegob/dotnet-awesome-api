# API

## Description

-   An E-Commerce platform where different products can be sold through shops
-   Users (Admins) can create shops and create products in those shops
-   Users (Clients) can register and login through Email & Password or third party apps (google)

## Running Locally

-   Install the .NET 6.0 sdk from [here](https://dotnet.microsoft.com/en-us/download).
-   Install the `dotnet-ef` tool for managing the database with `dotnet tool install --global dotnet-ef`.
-   Copy the contents of `appsettings.json` to a new file (`appsettings.Development.json`) and edit the contents
    accordingly.
-   Run the app
-   ```bash
    $ dotnet run
    # OR
    $ dotnet watch # With hot reload enabled
    ```

## Running in docker

-   ```bash
    $ docker-compose -f docker-compose/dev.docker-compose.yml up
    ```

## Global formatting rules

-   All formatting rules are defined in the `.editorconfig` file.
-   Jetbrains Rider and Visual Studio support it by default.
-   For VS Code, we need to install [this extension](https://marketplace.visualstudio.com/items?itemName=EditorConfig.EditorConfig)
