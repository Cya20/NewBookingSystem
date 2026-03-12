# Booking System API & Console Frontend
A decoupled, layered booking management system built with .NET 10. This project demonstrates a clean separation of concerns, moving from a persistent Data Access Layer through a Business Logic Layer to a Web API and a dedicated Console Client.

🏗 Architecture Overview
The solution is divided into four main projects to ensure maintainability and testability:

BookingWeb.API.DAL (Data Access Layer): * Uses Entity Framework Core with an In-Memory SQL Server provider.

Implements the Repository Design Pattern to abstract database operations.

BookingWeb.API.BLL (Business Logic Layer):

Follows the Interface & Service Pattern (IBookingBusinessLogicService / BookingBusinessLogicService).

Handles validation and coordinates data between the API and the Repository.

BookingWeb.API:

The entry point for the backend.

Exposes RESTful endpoints for Creating and Updating bookings.

Integrated with OpenAPI (Swagger) for interactive documentation.

Booking.Frontend.Console (Frontend):

An interactive CLI tool that consumes the Web API.

Allows users to view, create, and update bookings via HttpClient.

Booking.UnitTest:

A unit testing project targeting the BLL and Repository logic.

🚀 Getting Started
Prerequisites
.NET 10 SDK

Visual Studio 2026  or VS Code

Running the Application
To see the full "Full Stack" experience, you need to run both the API and the Console app:

Open the Solution in Visual Studio.

Right-click the Solution > Configure Startup Projects.

Select Multiple Startup Projects.

Set both BookingApp.WebAPI and BookingApp.Console to Start.

Press F5 / Start.

API Documentation
Once the API is running, you can explore the endpoints at:
--https
https://localhost:7016/swagger

--https
http://localhost:5127

🛠 Features
Enum Integration: Uses BookingItemType (Apartment, Vehicle, Show, Activity) throughout the stack.

Fluent API Client: The console app uses System.Net.Http.Json for seamless communication.

In-Memory Storage: No SQL Server installation required;

String-Based Enums: Configured via JsonStringEnumConverter so Swagger displays human-readable types instead of integers.

🧪 Testing
The unit tests cover the core business logic. To run them:

-- dotnet test
The tests verify:

Booking creation logic.

Proper mapping of Enums within the service layer.

📝 Commit History & Standards
This project follows standard Git flow. Each feature is committed with descriptive comments to track the evolution of the Repository and Service patterns.