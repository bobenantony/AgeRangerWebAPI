using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgeRangerWebAPI.Services;
using AutoMapper;
using AgeRangerWebAPI.Models;
using AgeRangerWebAPI.Entities;
using Microsoft.AspNetCore.Cors;

namespace AgeRangerWebAPI.Controllers
{
   [Route("api/persons")]
   [EnableCors("MyPolicy1")]
   public class PersonsController : Controller
    {
        private IPersonInfoRepository _personInfoRepository;

        public PersonsController(IPersonInfoRepository personInfoRepository)
        {
            _personInfoRepository = personInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetPersons()
        {
          var personEntities = _personInfoRepository.GetPersons();
          return Ok(personEntities);
        }

      [HttpPost()]
      [Route("Search")]
      public IActionResult Search([FromBody] PersonSearch search)
      {
         IActionResult ret = null;
         var personEntities = _personInfoRepository.GetPersons(search);

         if (personEntities.Count > 0)
         {
            ret = Ok(personEntities);
         }
         else
         {
            ret = NotFound();
         }

         return ret;
      }


      [HttpGet("{id}")]
      public IActionResult GetPerson(int id)
      {
         var person = _personInfoRepository.GetPerson(id);
        
         return Ok(person);
      }

      [HttpPut("{id}")]
      public IActionResult UpdatePerson(int Id, [FromBody] Person person)
      {
         if (person == null)
         {
            return BadRequest();
         }

         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

       
         if (!_personInfoRepository.SavePerson(person,"update"))
         {
            return StatusCode(500, "A problem happened while handling your request.");
         }

         return Ok(person);
      }

      [HttpPost()]
      public IActionResult CreatePerson(int Id,
            [FromBody] Person person)
      {
         if (person == null)
         {
            return BadRequest();
         }

         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         if (!_personInfoRepository.SavePerson(person,"insert"))
         {
            return StatusCode(500, "A problem happened while handling your request.");
         }

         return Ok(person);
      }

      [HttpDelete("{id}")]
      public IActionResult DeletePerson(int id)
      {
         _personInfoRepository.DeletePerson(id);

         if (!_personInfoRepository.Save())
         {
            return StatusCode(500, "A problem happened while handling your request.");
         }

         return NoContent();
      }
   }
}
