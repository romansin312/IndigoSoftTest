## Preparing
1. Create a new database on your PostgreSQL server.
2. Reaplce the connection string in the "appsettings.json" file (IndigoSoftTest.Api project).
3. Make sure that the migration is applied on the first run. There 4 tables should be created in your database.

## Run
Run the solution using IndigoSoftTest.Api as a startup project and "IndigoSoftTest.Api: http" default configuration. You can use Swagger UI: http://localhost:5067/swagger/index.html
