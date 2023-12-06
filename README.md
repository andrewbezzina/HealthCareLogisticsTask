# HealthCareLogisticsTask

## Summary

I used the code first approach with entity framework to create the data layer. Apart from the main Medication Tables I created seperate tables for ActiveIngredients, ATCCodes, Classifcations, PharmaceuticalForms, TherapeuticClasses and Units, as these values are often repeated and in common between different Medications.
I tried to use the clean architecture pattern (without Mediator) as much as possible. But the application layer is dependent on the infrastructure layer as I did not have time to create an abstractions for the repository layer.
The solution was written on VS 2022 using .NET 8 framework. I used controllers for the API, but the way forward is probably to use minimal APIs instead.

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
2. You can use swagger to acces the API.  
3. on the first run you need to populate the database from the excel file given by executing Post api/Medications/PopulateDatabase.
4. All the expected Medication CRUD methods are available on swagger as well.

### Api notes
For Update operation every field is nullable. if a field is left null it will be left unchanged.

## Imporvements
These are things I would have done given more time on the task.

- Add Front end
- Add Crud operations for all other data tables.
- Add a repository abstraction layer to improve seperation of concerns.
- Add Containerization
- Add Authorization
- improve error handling. For example right now I always return bad request type even if problem is server side.
- Use compiled queries to improve performance.