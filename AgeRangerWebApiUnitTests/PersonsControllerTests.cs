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

namespace AgeRangerWebApiUnitTests
{
    [TestClass]
    public class PersonsControllerTests
    {
      private PersonsController _controller;
      private Mock<IPersonInfoRepository> _mockRepository;
      private PersonDto personDtoObj;

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
      }

      /* Method Testing - GetPerson()  
       * Test - Checking the result for 'a valid ID'
       * Expected result 'should not be null'
       */
      [TestMethod]
        public void GetPerson_ValidPersonEntry_ShouldNotBeNull()
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
   }
}
