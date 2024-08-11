##Overview

NWUTechTrends is a web API developed using ASP.NET Core, designed to track and manage job telemetry data for various projects. The project leverages modern software development practices, including JWT authentication, to ensure secure access to the API endpoints.


##Features

    JWT Authentication: Secures the API using JSON Web Tokens.
    CRUD Operations: Provides Create, Read, Update, and Delete operations for job telemetry data.
    Project Management: Allows managing project data including related job telemetries.
    Savings Calculation: Calculates time and cost savings based on telemetry data.

##Getting Started
##Prerequisites

    .NET 8.0 SDK or later
    SQL Server (Azure SQL or Local SQL Server)
    Entity Framework Core
    Visual Studio 2022 or later (optional, but recommended)
  
  
  ##Installation
  
    1.Clone the Repository
    2.Set Up the Database
    3.Run the Application
    4.Access the Swagger UI
    Authentication

##The project uses JWT (JSON Web Token) for authentication. Below are the key points:

    JWT Setup: Configured in Program.cs with the necessary parameters for token validation.
    Bearer Authentication: Secures the endpoints using the Bearer scheme.
    Swagger Integration: JWT tokens can be tested via Swagger UI by providing the token in the "Authorize" button.

    </br>

## HTTP Methods 
The API supports the following HTTP methods: </br>
```GET     ›   Retrieve data``` </br>
```POST    ›   Create new records``` </br>
```PUT     ›   Update existing records``` </br>
```DELETE  ›   Delete records``` </br>
