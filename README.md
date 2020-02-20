# Fix IT Tracker API

## Description
This is a simple web API with basic CRUD operations used to interact with a database that keeps track of electronic repairs.  The main ambition of the project is to demonstrate my abilities in creating RESTful services in .NET.

## How to Build in Windows
1.	Download and install the dotnet-sdk exe from this link: https://tinyurl.com/veespp5 

2.	Next download the zip file for the source code and extract it from this link: https://tinyurl.com/t73s4x7 

3.	Now open PowerShell and navigate to where the **.sln** file is located

4.	Run this command to install the Entity Framework Tool: 
> **`dotnet tool install --global dotnet-ef`**

5.	Run this command to build the SQLite database file: 
> **`dotnet ef database update`**

6.	Finally run this command to launch the app: 
> **`dotnet watch run`**

> Note: The database will be seeded with data on the first build

7.	Navigate to https://localhost:5001/swagger/
> Note: Ignore the certificate error messages
> You can test the API by expanding a call and selecting **Try it out**

## Technologies Used
- .NET Core 3.0
- Entity Framework Core Sqlite 3.1.2
- Entity Framework Core Sql Server 3.1.1
- AutoMapper 7.0
- Swashbuckle 5.0

## View the Domain Model
https://tinyurl.com/qm4sz66

## Current Stage of the Project
This project is currently a work in progress.  Right now, the following has been completed:

- Continuous Integration
- Entity Framework Core
- Domain model
- Test data for the database
- GET statements
- Domain Transfer Objects with Automapping
- Documentation using Swagger
- Repository Pattern

## Coming Soon to the Project
- Unit Testing
- Error Handling
- POST, PATCH, PUT, DELETE statements
- Filtering and Paging
- Security and Authentication
- Front End Application (SPA)
