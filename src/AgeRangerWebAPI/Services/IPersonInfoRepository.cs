using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgeRangerWebAPI.Entities;
using AgeRangerWebAPI.Models;

namespace AgeRangerWebAPI.Services
{
    public interface IPersonInfoRepository
    {
      IList<PersonDto> GetPersons();
      IList<PersonDto> GetPersons(PersonSearch search);
      PersonDto GetPerson(int id);
      bool Save();
      bool SavePerson(Person person,String action);
      void DeletePerson(int id);
   }
}
