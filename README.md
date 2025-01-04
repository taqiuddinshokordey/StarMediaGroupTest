# StarMediaGroupTest

This is the README file for the StarMediaGroupTest application.

## How to Run

To run the application, use the following command:

```bash
dotnet run
```

Make sure you are in the project directory before running the command.

## Restore Dependencies

To restore the dependencies, use the following command:

```bash
dotnet restore
```

## Database Setup

1. Set up Entity Framework CLI tools:
   ```bash
   # Install EF CLI tools globally
   dotnet tool install --global dotnet-ef
   
   # Verify installation
   dotnet ef --version
   
   # If you need to update existing tools
   dotnet tool update --global dotnet-ef
   ```

2. Install required NuGet packages in your project:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.Design
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   ```

3. Update your database connection in `appsettings.json`:
   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
       }
   }
   ```

4. Create and update the database:
   ```bash
   # Create a new migration
   dotnet ef migrations add InitialCreate
   
   # Apply migrations to the database
   dotnet ef database update
   ```

Ensure that you have the Entity Framework CLI tools installed and configured.

## Adding a Connection

To add a connection, follow these steps:

1. Open the `appsettings.json` file located in the project directory.
2. Add your connection string to the `ConnectionStrings` section. For example:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "YourConnectionStringHere"
    }
}
```

3. Save the `appsettings.json` file.

Now your application is configured to use the specified connection string.

## Project Structure

The project structure is as follows:

```
StarMediaGroupTest/
├── Controllers/
│   └── HomeController.cs
├── Models/
│   └── SampleModel.cs
├── Views/
│   └── Home/
│       └── Index.cshtml
├── appsettings.json
├── Program.cs
├── Startup.cs
└── README.md
```


## Containerize with Docker

To containerize the application using Docker, follow these steps:

1. Create a `Dockerfile` in the project directory with the following content:

    ```dockerfile
    FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
    WORKDIR /app
    EXPOSE 80

    FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
    WORKDIR /src
    COPY ["StarMediaGroupTest/StarMediaGroupTest.csproj", "StarMediaGroupTest/"]
    RUN dotnet restore "StarMediaGroupTest/StarMediaGroupTest.csproj"
    COPY . .
    WORKDIR "/src/StarMediaGroupTest"
    RUN dotnet build "StarMediaGroupTest.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "StarMediaGroupTest.csproj" -c Release -o /app/publish

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "StarMediaGroupTest.dll"]
    ```

2. Build the Docker image using the following command:

    ```bash
    docker build -t starmediagrouptest .
    ```

3. Run the Docker container using the following command:

    ```bash
    docker run -d -p 8080:80 --name starmediagrouptest_container starmediagrouptest
    ```

Now your application is running inside a Docker container and can be accessed at `http://localhost:8080`.

## License

This project is licensed under the MIT License.