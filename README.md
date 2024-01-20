# Portfolio Backend

## Overview
This repository contains the backend code for a portfolio website. It's designed to manage personal information, project details, and handle admin authentication and email services.

## Features
- Admin Authentication: Secure login functionality for the admin user.
- Personal Information Management: API endpoints to retrieve and update personal information.
- Project Information Management: API endpoints to create, retrieve, update, and delete project information.
- Email Service: Functionality to send emails through the website.

## Technology Stack
- C#: The backend is written in C#.
- ASP.NET Core: Used for creating the web API.
- Dapper: Used for database operations.
- Node.js: Used for seeding and extracting data from the production database.
- PostgreSQL: The primary database for storing data.
- Docker: Containerization of the application.
- JWT Authentication: For securing the API endpoints.
- Fluent Migrator: For database migrations.
- Firebase: For storing images.
- MailKit: For sending emails.
- GitHub Actions: For continuous integration and continuous deployment.
- AWS EC2: For hosting the application.
- Docker: For containerization of the application.
- Swagger: For API documentation.

## Getting Started
- #### Prerequisites: Ensure you have .NET Core and PostgreSQL installed.
- #### Configuration: Set up the necessary configuration in the appsettings.json file.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<host>;Port=<port>;Database=<database_name>;User Id=<usernamme>;Password=<password>;"
  },
  "Jwt": {
    "Key": "<secret-key>",
    "Issuer": "<issuer>",
    "Audience": "<audience>"
  },
 
  "Email": {
    "Host": "<email-host>",
    "Port": "<port AS INTEGER>",
    "Username": "<username>",
    "Password": "<password>",
    "From": "<email>",
    "To": "<email>"
    },

    "Firebase": {
    "ApiKey": "<api-key>",
    "AuthDomain": "<auth-domain>",
    "ProjectId": "<project-id>",
    "StorageBucket": "<bucket>",
    "MessagingSenderId": "<sender-id>",
    "AppId": "<app-id>",
    "MeasurementId": "<measurement-id>"
    },

    "AllowedHosts": "*"
}
```
#### Database Migration: Run the Fluent Migrator to set up your database.
- Migrations run automatically on application startup for the development and test environments.
- For the production environment seedint and extraction use the Node.js scripts in the extractSeedDb directory.

### Running the Application: Use dotnet run to start the application.

## Testing
- #### The portfolio.test directory contains tests for various components of the application.

## Continuous Integration and Deployment
- #### This application uses GitHub Actions for continuous integration and deployment. Refer to the .github/workflows directory for more details.
 
## The CICD flow is as follows:
1. #### Make a pull request to the main branch.
2. #### GitHub Actions will run the tests.
3. #### If tests pass and branch is merged deployment will be triggered.
4. #### appsettings.json.dec file will be decrypted.
5. #### GitHub Actions will build the application and push the docker image to Docker Hub.
6. #### Deployment script will be copied to the EC2 instance.
7. #### Deployment script will be run on the EC2 instance.
8. #### Deployment script will pull the latest image from Docker Hub and run the container.
