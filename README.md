# Contacts App

A full-stack web application for managing contacts, built with:

- **Backend**: ASP.NET Core 8
- **Frontend**: Vue.js
- **Database**: SQL Server

---

## Prerequisites

Before running the application, make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js (LTS version)](https://nodejs.org/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

## Setup Instructions

### 1. Clone the Repository
### 2. Restore the Database in SQL Server
Navigate to the contacts-db.bak file in the root of the cloned repository
### 3. Run the Backend (ASP.NET Core API)

```bash
cd Contacts.API
dotnet restore
dotnet build
dotnet run
```
By default, the backend will be available at:

https://localhost:7157 , http://localhost:5183

### 4. Run the Frontend (Vue.js)
```bash
cd ../contacts-client
npm install
npm run dev
```
The frontend will run by default at:
http://localhost:5173



