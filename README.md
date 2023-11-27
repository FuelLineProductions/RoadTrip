# RoadTrip

## Basic Setup For Development Environment

- Make a copy of appsettings.json and name it appsettings.Development.json
- Set up local database connections under `ConnectionStrings`:
    - Identity DB under `RoadTripIdentityDb`
    - Content DB under `RoadTripDb`
    - Log DB under `LogDb`
    - It is recommended that `MultipleActiveResultSets=true;` is setup in your connection strings otherwise you may run into threading issues
- Set up local log filed under "LogFile" Setting
    - This should be a filepath on your machine that the code will have access to, remember to use the file name prefix
    - Example: `D:\\RoadTrip\\Logs\\RoadTripLog_.txt`
- Next open up package manager console (assuming you are using Visual Studio, otherwise you will need to get the syntax for what you are using)
    -	    update-database -Context RoadTripContextDb
	-       update-database -Context RoadTripIdentityDb
    - This will update your DBs you setup in appsettings to contain the migrations needed to login and add your own data

- Use the following sql to create the Log DB
     - https://github.com/serilog-mssql/serilog-sinks-mssqlserver#table-definition
    - For development you may only need to use the Table Definition if you use a local DB
However if you need to setup permissions there is a basic guide on this page too

# Project Makeup

- .NET 8 
- C#
- Blazor Server
- SignalR
- Unit testing using FluentAssertions under xUnit
- EF Core using MS SQL Server (Code first)
- Serilog for logging
    - Sinks:
        - Console 
        - File
        - MS SQL Server
- Service and repository pattern