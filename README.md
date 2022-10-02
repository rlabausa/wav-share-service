# wav-share-service
This is a .NET Core Web API for storing base64-encoded audio files.  This project contains examples of:
- Extensions Methods for configuring a `WebApplication` via `IServiceCollection` and `IApplicationBuilder` 
- Global error handling/request logging
- Custom Swagger documentation with `Data Annotations` and `ProducesResponseType` decorators
- Consistent HTTP error responses with `InvalidModelStateResponseFactory` options
- Data validation with a Business Logic Layer (BLL)
- Data retrieval with a Data Access Layer (DAL)
- Returning a created resource from a POST method
- Transact-SQL scripts for basic CRUD operations

## Running Locally
Before the service will be able to successfully retrieve data, a MS SQL Server database must be set up.  This can be achieved by executing the T-SQL scripts in the [sql](/sql) folder on a MS SQL database server (in my case, I've used a local database server, i.e., localhost) and replace the connection strings in the **appsettings.*.json** files if necessary.  

Once the database is set up, data retrieval can be tested by launching the service in Debug mode and using Postman or Swagger to GET requests to the appropriate endpoints.


## Swagger
![WavShare API Overview](/img/WavShareService_Swagger_Overview.jpeg)
