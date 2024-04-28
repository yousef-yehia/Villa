# Villa Management System
Villa Management System is an ASP.NET Core API and MVC project designed to manage villas. It provides functionalities for creating, deleting, updating, and viewing villas. Additionally, users can sign up and log in to access the system.

## Features
Create: Users can create new villa entries with details like name, location, description, etc.
Read: View details of existing villas.
Update: Modify information about existing villas.
Delete: Remove villa entries from the system.
Authentication: Users can sign up and log in to access the system securely.
Authorization: Role-based access control to restrict certain functionalities to specific user roles.
JWT Token: Authentication is implemented using JSON Web Tokens (JWT) for secure communication between client and server.
Repository Pattern: The project follows the repository pattern for data access, making it easier to manage data interactions.
Generic Repository: Utilizes a generic repository to provide a consistent interface for accessing data from various entities.
Auto Mapper: Auto Mapper library is used for mapping between different object types, simplifying data transfer.

## Technologies Used
ASP.NET Core: Framework for building APIs and web applications.
Entity Framework Core: ORM for interacting with the database.
SQL Server: Relational database management system used for data storage.
JWT Authentication: JSON Web Tokens for secure authentication.
Auto Mapper: Library for object-to-object mapping.
Repository Pattern: Design pattern for separating data access logic.
Generic Repository: Generic implementation of the repository pattern for data access.

## Setup Instructions
1. Clone the repository to your local machine.
2. Open the solution in Visual Studio or your preferred IDE.
3. Make sure you have SQL Server installed and running.
4. Update the connection string in appsettings.json to point to your SQL Server instance.
5. Run the database migrations to create the necessary tables: dotnet ef database update.
6. Build and run the project.

## Usage
API Endpoints: Explore the available API endpoints for managing villas using tools like Postman or Swagger.
MVC Interface: Access the MVC interface in your browser to interact with the system visually.
