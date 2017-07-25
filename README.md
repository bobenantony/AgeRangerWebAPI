# AgeRangerWebAPI

API implemented using ASP.net Core Web API . 
It uses EF Core & LINQ for the database access and Manipulations and follows Repository pattern

API exposes the following Http requests/services - GET,PUT,POST & DELETE 

When ever a call/service request comes from the Client , It call the respective servive in the controller .
The controller in turn calls the corresponding repository functionality for fetching the data using the 
database context of EF Core

Code duplication is avoided with CommonServices class which is static class , gives more consistency to the code

OOP concepts are followed while defining the entities

Solution uses a third party middleware - AutoMapper .It is a simple little library built to map one object to another. 
Here it helps mapping between Person entity and PerstoDto

Brief comment lines are added for the functionalities in Repositories , Common services & start up classes to help 
code review easy
