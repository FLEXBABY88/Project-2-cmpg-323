# CMPG323 Project 2 42019222

![image](https://github.com/FLEXBABY88/Project-2-cmpg-323/blob/main/API_LOGO.jpg)
NWUTechTrends is a web API developed using ASP.NET Core, designed to track and manage job telemetry data for various projects. The project leverages modern software development practices, including JWT authentication, to ensure secure access to the API endpoints.


##Key Features
JWT Authentication: Robust security through JSON Web Tokens, ensuring that only authorized users can access the API.
Comprehensive CRUD Operations: Seamlessly create, read, update, and delete job telemetry data, all through our intuitive API endpoints.
Project Management: Efficiently manage your project data, including related job telemetries, all in one place.
Savings Calculation: Automatically calculate time and cost savings based on the telemetry data, giving you valuable insights.

# API Usage
Authentication and API Access:
To get started with NWUTechTrends, simply register an account on the platform. After registration, log in with your credentials at the login endpoint. Once authenticated, you'll receive a token, which grants you secure access to the API’s features.


# Requirements
.NET 8.0 SDK or later
SQL Server (Azure SQL or Local SQL Server)
Entity Framework Core
Visual Studio 2022 or later (Optional, but recommended)
  
  
##Installation
  
1.Clone the Repository
2.Set Up the Database
3.Run the Application
4.Access the Swagger UI
Authentication

##Authentication

This project uses JWT (JSON Web Token) for authentication. Below are the key aspects:

    JWT Setup: Configured in Program.cs with essential parameters for token validation.
    Bearer Authentication: All endpoints are secured using the Bearer scheme.
    Swagger Integration: Easily test JWT tokens via Swagger UI by inputting the token in the "Authorize" button.

NWUTechTrends is your reliable solution for efficient and secure job telemetry management. Dive in and experience the future of project management with us!

    </br>

## HTTP Methods 
The API supports the following HTTP methods: </br>
```GET     ›   Retrieve data``` </br>
```POST    ›   Create new records``` </br>
```PUT     ›   Update existing records``` </br>
```DELETE  ›   Delete records``` </br>


# Endpoints
## Telemetry
Manage telemetry data, including creating, updating, retrieving, and deleting telemetry records.

|        Function                                  |                              Features                                                                                |
|--------------------------------------------------|----------------------------------------------------------------------------------------------------------------------|
|```GET /api/telemetry```                          |  Retrieve a list of all telemetry records.                                                                           |
|```GET /api/telemetry/{id}```                     |Retrieve details of a specific telemetry record by ID.                                                                |
|```POST /api/telemetry```                         |Create a new telemetry record. Payload should contain telemetry information.                                          |
|```PUT /api/telemetry/{id}```                     |Update an existing telemetry record by ID. Payload should contain updated information.                                | 
|```DELETE /api/telemetry/{id}```                  | Delete a telemetry record by ID.                                                                                     |
|```GET /api/telemetry/savings/business-function```| Get savings data filtered by business function. Requires businessFunction, startDate,and endDate as query parameters.|
|```GET /api/telemetry/savings/client```           | Get savings data filtered by client. Requires clientId, startDate, and endDate as query parameters.                  |

</br>
