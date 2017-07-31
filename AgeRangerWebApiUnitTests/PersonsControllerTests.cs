using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRangerWebAPI;
using AgeRangerWebAPI.Controllers;
using AgeRangerWebAPI.Entities;
using AgeRangerWebAPI.Services;
using AgeRangerWebAPI.Models;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AgeRangerWebApiUnitTests
{
    [TestClass]
    public class PersonsControllerTests
    {
      private PersonsController _controller;
      private Mock<IPersonInfoRepository> _mockRepository;
      private PersonDto personDtoObj;
      private List<PersonDto> personDtoList;

      [TestInitialize]
      public void TestInitialize()
      {
         _mockRepository = new Mock<IPersonInfoRepository>();

         /*Initializing a single PersonDtoObj*/
         personDtoObj = new PersonDto()
         {
            Id = 1,
            FirstName = "Boben",
            LastName = "Antony",
            Age = 37,
            AgeGroup = "Very Adult"
         };

         /*Initializing a list of PersonDtoObj*/
         personDtoList = new List<PersonDto>() {
            new PersonDto() {Id = 1, FirstName = "Boben", LastName = "Antony", Age = 37, AgeGroup = "Very Adult" }
         };
      }

      /* Method Testing - GetPerson()  
       * Test - Checking the result for 'a valid ID'
       * Expected result 'should not be null'
       */
      [TestMethod]
        public void GetPerson_ValidPersonID_ShouldNotBeNull()
        {
        _mockRepository.Setup(x => x.GetPerson(It.IsAny<int>())).Returns(personDtoObj);

         _controller = new PersonsController(_mockRepository.Object);
         _controller.GetPerson(2);
         _controller.Should().NotBeNull();
      }

      /* Method Testing - GetPerson()  
       * Test - Checking the result for 'No Person With Given Id Exists'
       * Expected result should be 'Not Found'
       */

      [TestMethod]
      public void GetPerson_NoPersonWithGivenIdExists_ShouldReturnNotFound()
      {
         _controller = new PersonsController(_mockRepository.Object);
         var result = _controller.GetPerson(2);

         result.Should().BeOfType<NotFoundResult>();
      }

      /* Method Testing - GetPersons()  
       * Test - Checking the result for empty parameter and valid object
       * Expected result 'should not be null'
       */
      [TestMethod]
      public void GetPersons_ValidObject_ShouldNotBeNull()
      {
         _mockRepository.Setup(x => x.GetPersons()).Returns(personDtoList);

         _controller = new PersonsController(_mockRepository.Object);
         _controller.GetPersons();
         _controller.Should().NotBeNull();
      }

      /* Method Testing - GetPersons()  
       * Test - Checking the result for empty parameter and empty person
       * Expected result 'should Return Not Found'
       */
      [TestMethod]
      public void GetPersons_NullObject_ShouldReturnNotFound()
      {
         personDtoList = new List<PersonDto>() { };
         _mockRepository.Setup(x => x.GetPersons()).Returns(personDtoList);

         _controller = new PersonsController(_mockRepository.Object);
         _controller.GetPersons();
         _controller.Should().BeOfType<NotFoundResult>();
      }
   }
}
