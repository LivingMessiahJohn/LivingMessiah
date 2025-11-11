# LivingMessiah Sukkot Database Project

This SQL Server Database SDK project contains the schema and database objects for the Sukkot application database.

## Overview

The Sukkot database is used by the LivingMessiahSukkot and LivingMessiahAdmin applications to store event registration, donation tracking, and error logging information.

## Project Structure

```
LivingMessiah.Sukkot.Database/
├── Tables/              # Database tables
│   └── ErrorLog.sql    # Error logging table
├── Views/              # Database views
│   └── zvwErrorLog.sql # Error log view
├── StoredProcedures/   # Stored procedures
│   ├── stpLogErrorTest.sql  # Test error logging
│   └── stpLogErrorEmpty.sql # Clear error log
└── Functions/          # User-defined functions
```

## Building the Project

### Prerequisites

- .NET 9.0 SDK or later
- Microsoft.Build.Sql SDK (automatically restored via NuGet)

### Build Commands

```bash
# Build the database project
dotnet build LivingMessiah.Sukkot.Database/LivingMessiah.Sukkot.Database.sqlproj

# Build the entire solution (including database)
dotnet build LivingMessiah.sln
```

## Publishing the Database

The project can be published to generate a DACPAC file for deployment:

```bash
dotnet publish LivingMessiah.Sukkot.Database/LivingMessiah.Sukkot.Database.sqlproj -o ./publish/database
```

The DACPAC file can then be deployed using:
- SQL Server Data Tools (SSDT)
- SqlPackage.exe command-line tool
- Azure DevOps pipelines
- GitHub Actions workflows

## CI/CD Integration

This project is integrated into the GitHub Actions workflow for automated building and deployment. The workflow:

1. Builds the database project along with other projects
2. Generates a DACPAC file
3. Can be deployed to Azure SQL Database or SQL Server instances

## Connection String

The application uses the following connection string format:

```json
"ConnectionStrings": {
  "Sukkot": "Data Source=SERVER;Initial Catalog=Sukkot;Integrated Security=True;TrustServerCertificate=True;"
}
```

## Development

### Adding New Database Objects

1. Create a new `.sql` file in the appropriate folder (Tables, Views, StoredProcedures, or Functions)
2. Add the file reference to `LivingMessiah.Sukkot.Database.sqlproj` in the `<ItemGroup>` section:
   ```xml
   <Build Include="Tables\YourNewTable.sql" />
   ```
3. Build the project to validate the changes

### Schema Versioning

All database schema changes should be:
- Version controlled in this project
- Tested locally before committing
- Deployed through the CI/CD pipeline

## Related Projects

- **LivingMessiahSukkot**: Main Sukkot event registration application
- **LivingMessiahAdmin**: Administrative interface for managing data
- **LivingMessiah**: Main application

## Support

For questions or issues related to the database schema, please contact the development team.
