# Fine Haven Spa & Wellness - CS212 Final Project

## Overview
Fine Haven Spa & Wellness is a web application designed for managing spa and wellness services, appointments, and user accounts. Built as a final project for CS212, this application demonstrates a full-stack approach using ASP.NET Core with Razor Components (Blazor), Entity Framework Core, and a modern UI.

## Features
- User authentication (Sign Up, Sign In, Sign Out)
- Role-based access (Admin, Staff, Client)
- Account management (Create, Edit, View)
- Service management (Create, Edit, View services)
- Appointment scheduling and management
- Responsive navigation and layout
- Seed data for initial setup

## Project Structure
```
Components/           # Razor components for UI
  Layout/             # Main layout and navigation
  Pages/              # Page components (Home, Auth, Dashboard, etc.)
	 LoggedIn/         # Authenticated user pages
		AccountPages/   # Account management
		AppointmentPages/ # Appointment management
		ServicePages/   # Service management
Data/                 # Database context and seed data
Migrations/           # Entity Framework Core migrations
Models/               # Data models (User, Service, Appointment, etc.)
Shared/               # Interfaces and services
wwwroot/              # Static files (CSS, JS, Bootstrap)
Program.cs            # Application entry point
appsettings.json      # Configuration files
```

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or use SQLite by modifying the connection string)

### Setup
1. **Clone the repository:**
	```sh
	git clone <repo-url>
	cd fine-haven-spa_wellness
	```
2. **Restore dependencies:**
	```sh
	dotnet restore
	```
3. **Apply migrations and seed the database:**
	```sh
	dotnet ef database update
	```
4. **Run the application:**
	```sh
	dotnet run
	```
5. **Open in browser:**
	Navigate to `https://localhost:5001` (or the URL shown in the terminal)

## Configuration
- Update `appsettings.json` and `appsettings.Development.json` for database connection strings and other settings.
- Launch profiles are in `Properties/launchSettings.json`.

## Technologies Used
- ASP.NET Core (Blazor Server)
- Entity Framework Core
- Bootstrap (UI styling)
- C#

## Folder Details
- **Components/**: UI components and pages
- **Data/**: Database context and seeding
- **Models/**: Entity models
- **Shared/**: Service interfaces and implementations
- **wwwroot/**: Static assets

## Authors
- [Efe Awo-Osagie](https://www.linkedin.com/in/efe-awo-osagie-b4b796381/)
- [Jack Losing](https://www.linkedin.com/in/jlosing/)
- [Jesse Dahlke](jesse.dahlke@ndsu.edu)
- [Timmothy Middleton](timothy.middleton@ndsu.edu)

## License
This project is for educational purposes. See your institution's guidelines for code sharing and reuse.

---
*CS212 Final Project - Fine Haven Spa & Wellness*