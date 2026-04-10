# Online Chat Backend

ASP.NET Core backend for the Online Chat application.

## Overview

This backend provides:

- REST API for chat-related operations
- real-time messaging with SignalR
- Azure SignalR Service integration
- Azure SQL Database persistence
- Azure AI Text Analytics sentiment analysis
- Swagger/OpenAPI documentation

## Tech Stack

- ASP.NET Core
- Entity Framework Core
- Azure SQL Database
- Azure SignalR Service
- Azure AI Text Analytics
- Swagger / OpenAPI

## Features

- user-related endpoints
- chat room management
- message persistence in Azure SQL
- real-time message delivery via SignalR
- optional sentiment analysis for messages
- API documentation with Swagger

## Project Structure

Typical important folders and files:

- `Data/` - database context and data access setup
- `Hubs/` - SignalR hubs
- `Repositories/` - repository implementations
- `Services/` - business logic and integrations
- `Interfaces/` - contracts for services and repositories
- `Controllers/` - API endpoints
- `Program.cs` - application startup and DI configuration

## Prerequisites

Before running the project, make sure you have:

- .NET SDK installed
- Azure SQL Database created
- Azure SignalR Service created
- Azure AI Text Analytics resource created
- Redis instance available (optional, if enabled in the app)

## Configuration

Application settings are provided through `appsettings.json`, secrets, or Azure App Service configuration.

### Project configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<your-db-connection>",
    "AzureSignalR": "<azure-signalr-connection-string>"
  },
  "AzureTextAnalytics": {
    "Endpoint": "<text-analytics-endpoint>",
    "ApiKey": "<text-analytics-api-key>"
  }
}
```

## Running Locally

### 1. Restore dependencies

```bash
dotnet restore
```

### 2. Run the project

```bash
dotnet watch run
```

### 3. Open Swagger

If Swagger is needed, open:

```text
http://<your domain>/swagger
```

## Database

This project uses **Azure SQL Database**.

Main points:

- EF Core is configured with `UseSqlServer(...)`
- messages and related data are stored in Azure SQL
- firewall rules must allow the client IP and Azure services when deployed

### Migrations

Apply migrations:

```bash
dotnet ef database update
```

## SignalR

The backend exposes a SignalR hub for real-time chat.

Route:

```text
/api/chat
```

## Deployment

The backend is intended to be deployed to **Azure App Service**.

### Required Azure configuration

Set these values in App Service:

#### Connection Strings

- `DefaultConnection` - type `SQLServer`
- `AzureSignalR` - type `Custom`

#### App Settings

- `AzureTextAnalytics__Endpoint`
- `AzureTextAnalytics__ApiKey`
