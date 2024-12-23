# Hotel API Project

This project is a .NET Core Web API designed for managing hotel information.

## Prerequisites

- .NET 8.0 SDK
- An IDE such as Visual Studio, VS Code, or Rider

## Project Setup

1. Clone the repository:
   ```
   git clone <repository-url>
   cd sunway-test
   ```

2. Restore NuGet packages:
   ```
   dotnet restore
   ```

## Running the Application

1. Start the application using:
   ```
   dotnet run
   ```

2. Access the Swagger UI in your browser at:
   - HTTP: `http://localhost:5175/swagger`
   - HTTPS: `https://localhost:7018/swagger`

## Project Structure

- `Program.cs`: The main application entry point and configuration.
- `hotels.json`: Contains sample hotel data.
- `Models/`: Directory for data models.
- `Services/`: Directory for business logic and services.



## Dependencies

The project includes the following main packages:
- Microsoft.AspNetCore.OpenApi (8.0.11)
- Swashbuckle.AspNetCore (6.6.2)


To view the complete API documentation and test the endpoints, navigate to `/swagger` when the application is running.