# AgeRangerWebAPI

API implemented using ASP.net Core Web API . 
It uses EF Core & LINQ for the database access and Manipulations and follows Repository pattern

API exposes the following Http requests/services - GET,PUT,POST & DELETE 

Whenever a call/service request comes from the Client , It calls the respective service in the controller .
The controller in turn calls the corresponding repository functionality for fetching the data using the 
database context of EF Core

Code duplication is avoided with CommonServices class which is a static class , gives more consistency to the code

OOP concepts are followed while defining the entities

Solution uses a third party middleware - AutoMapper .It is a simple little library built to map one object to another. 
Here it helps mapping between Person entity and PersonDto

Brief comment lines are added for the functionalities in Repositories , Common services & start up classes to help 
code review easy

Unit tests can be found in the project AgeRangerWebApiUnitTests Project
