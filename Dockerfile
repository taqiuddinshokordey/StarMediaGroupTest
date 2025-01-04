# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Publish the application
RUN dotnet publish -c Release -o /out

# Use the runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /out .

# Expose the application port (default ASP.NET Core port is 5000)
EXPOSE 80

# Specify the entry point for the container
ENTRYPOINT ["dotnet", "StarMediaGroupTest.dll"]
