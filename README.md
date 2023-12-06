# HealthCareLogisticsTask

## Intructions

### Setup Database
1. You will need a local SQL server engine. I use the built-in SQL server express 2019 Local DB.
2. Update the <code>Default</code> database connection string  that is found in *MedicationRecords.Api/appsettings.json* to reflect the engine you will be using.
3. Go to the **Package Manager Console** and select *MedicationRecords.Infrastructure* as the default project.
4. Run the following command to psetup database with existing Migration files: <code>Update-Database</code> 
5. Use Sql Server Managemnet Studio to confirm that a database called *MedicationsDB* was created and that it includes the following tables:
	- [dbo].[ActiveIngredients]
	- [dbo].[ATCCodes]
	- [dbo].[Classifications]
	- [dbo].[Medications]
	- [dbo].[PharmaceuticalForms]
	- [dbo].[TherapeuticClasses]
	- [dbo].[Units]
	
### Run tha Api
1. Run the application from VS 2022 make sure that *MedicationRecords.Api* is the startup project.
2. Execute Post api/Medications/PopulateDatabase

### Api notes
For Update operation every field is nullable. if a field is left null it will be left unchanged.

## Time Taken

- Backend part took about 10-12 hours
- Frontend part took about

## Imporvements
These are things I would have done given more time on the task.

- Add Crud operations for all other data tables.
- Add a repository abstraction layer to improve seperation of concerns.
- Add Containerization
- Add Authorization
- improve error handling. For example right now I always return bad request type even if problem is server side.
- use compiled queries to improve performance.