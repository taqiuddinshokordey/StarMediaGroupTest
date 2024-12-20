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

## Apply Migrations

To apply the migrations to the database, use the following command:

```bash
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

## License

This project is licensed under the MIT License.