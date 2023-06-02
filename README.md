# AirLegance App

AirLegance is a full-stack application that requires a number of dependencies to run correctly. This guide will take you step by step through the setup process.

## Prerequisites
Make sure you have the following installed on your machine before proceeding:

- Node.js
- SQL Server
- Visual Studio (for backend)
- Visual Studio Code (for frontend)
- Entity Framework Core (EF-Core) Toolkit

You can install EF-Core Toolkit via the command line with:

dotnet tool install --global dotnet-ef

## Setup and Installation

**Frontend Setup:**

1. Open a terminal and navigate to the frontend directory.
2. Run the following command to install necessary packages:

**Backend Setup:**

1. Go to the backend folder and open either `appsettings.json` or `appsettings.Development.json`.
2. Update the connection string with your SQL Server name.

This setup assumes you have a working knowledge of the command line interface, Node.js, and SQL Server. If you encounter any issues during the setup, please consult the official documentation for each tool or contact the maintainers.

Enjoy using AirLegance!