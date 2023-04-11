
The goal of this project is to be a kickstart to your .Net WebApi, implementing the most common used patterns
and technologies for a restful API in .net, making your work easier.

## Docker
## DEVELOPMENT
``docker compose --env-file .env.development up jiban-api-development --build --remove-orphans -d``
## PRODUCTION
``docker compose --env-file .env.production up  jiban-api-production --build --remove-orphans -d``
`dotnet clean WebApiBoilerplate.sln``
`dotnet build WebApiBoilerplate.sln``


1. Run ``docker-compose --env-file .env -f docker-compose.yml -f docker-compose.override.yml  up --build -d`` in 
the root directory, or, in visual studio, set the docker-compose project as startup and run. This should start the application and DB.
 - 1. For docker-compose, you should run this command on the root folder: ``dotnet dev-certs https -ep https/aspnetapp.pfx -p yourpassword``
		Replace "yourpassword" with something else in this command and the docker-compose.override.yml file.
This creates the https certificate.
2. Visit http://localhost:5000/api-docs or https://localhost:5001/api-docs to access the application's swagger.

## Running tests
**Important**: You need to have docker up and running. The integration tests will launch a SQL server container and use it to test the API.

In the root folder, run ``dotnet test``. This command will try to find all test projects associated with the sln file.
If you are using Visual Studio, you can also access the Test Menu and open the Test Explorer, where you can see all tests and run all of them or one specifically. 

After that, you can pass the jwt on the lock (if using swagger) or via the Authorization header on a http request.


# Project Structure
1. Services
	- This folder stores your apis and any project that sends data to your users.
	1. Boilerplate.Api
		- This is the main api project. Here are all the controllers and initialization for the api that will be used.
	2. docker-compose
		- This project exists to allow you to run docker-compose with Visual Studio. It contains a reference to the docker-compose file and will build all the projects dependencies and run it.
2. Application
	-  This folder stores all data transformations between your api and your domain layer. It also contains your business logic.
	1. Auth
		- This folder contains the login Session implementation.
3. Domain
	- This folder contains your business models, enums and common interfaces.
	1. Boilerplate.Domain
		- Contains business models and enums.
		1. Auth
			- This folder contains the login Session Interface.
4. Infra
	- This folder contains all data access configuration, database contexts, anything that reaches for outside data.
	1. Boilerplate.Infrastructure
		- This project contains the dbcontext, entities configuration and migrations.


#Only Docker Compose Remote Debugging
1. ``dotnet run --project src/Boilerplate.Api/Boilerplate.Api.csproj``
2. ``dotnet build WebApiBoilerplate.sln``

# Migrations
1. To run migrations on this project, run the following command on the root folder: 
	- ``dotnet ef migrations add AspNetUsers --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\ --context ApplicationDbContext``


2. This command will set the entrypoint for the migration (the responsible to selecting the dbprovider { sqlserver, mysql, etc } and the connection string) and the selected project will be "Boilerplate.Infrastructure", which is where the dbcontext is.

3. Rollback all migrations
	- ``dotnet ef database update 0 --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\ --context ApplicationDbContext``
	- ``dotnet ef database update 20230411122514_Previous --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\ --context ApplicationDbContext``
	
4. Reverse Enginering Database (Remove Schema public)
	- `` dotnet ef dbcontext scaffold "Host=172.16.20.4;Database=madsisqa;Username=raul.flores;Password=Per aspera$" Npgsql.EntityFrameworkCore.PostgreSQL --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\ --output-dir .\Reverse ``
	- `` dotnet ef dbcontext scaffold "Server=localhost;Database=Jiban;User Id=sa;Password=Yourpassword123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\ --output-dir .\Reverse ``

# Thanks
This project has great influence of https://github.com/lkurzyniec/netcore-boilerplate and https://github.com/EduardoPires/EquinoxProject. If you have time, please visit these repos, and give them a star, too!

# About
This Jiban Platform was developed by Jiban Developers under [MIT license](LICENSE).
