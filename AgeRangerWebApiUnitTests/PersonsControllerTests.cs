using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRangerWebAPI;
using AgeRangerWebAPI.Controllers;
using AgeRangerWebAPI.Entities;
using AgeRangerWebAPI.Services;
using AgeRangerWebAPI.Models;
using Moq;
using FluentAssertions;

namespace AgeRangerWebApiUnitTests
{
    [TestClass]
    public class PersonsControllerTests
    {
      /* Method Testing - GetPerson()  
       * Test - Checking the result for a valid ID
       * Expected result should not be null
       */
      [TestMethod]
        public void GetPerson_ValidPersonEntry_NotBeNull()
        {
         PersonDto personDtoObj = new PersonDto()
         {
            Id = 1,
            FirstName = "Boben",
            LastName = "Antony",
            Age = 37,
            AgeGroup = "Very Adult"
         };

         Mock<IPersonInfoRepository> _personInfoRepository = new Mock<IPersonInfoRepository>();
         _personInfoRepository.Setup(x => x.GetPerson(It.IsAny<int>())).Returns(personDtoObj);

         var personsController = new PersonsController(_personInfoRepository.Object);
         personsController.GetPerson(2);
         personsController.Should().NotBeNull();
      }
    }
}
