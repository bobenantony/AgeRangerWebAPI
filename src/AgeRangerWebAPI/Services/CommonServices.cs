using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgeRangerWebAPI.Entities;
using AgeRangerWebAPI.Models;

namespace AgeRangerWebAPI.Services
{
    public static class CommonServices
    {
        public static List<PersonDto> GetPersonsDto(List<Person> personList , List<AgeGroup> ageGroupList)
        {
            List<PersonDto> personDtoList = new List<PersonDto>();

            for (var i = 0; i < personList.Count; i++)
            {
                personDtoList.Add(new PersonDto()
                {
                    Id = personList[i].Id,
                    FirstName = personList[i].FirstName,
                    LastName = personList[i].LastName,
                    Age = personList[i].Age,
                    AgeGroup = AgeGroupDescription(personList[i].Age, ageGroupList)
                });
            }
            return personDtoList;
        }

        public static string AgeGroupDescription (int age , List<AgeGroup> ageGroupList)
        {
            return ageGroupList.Where(a => a.MinAge <= age && a.MaxAge > age).FirstOrDefault().Description;
        }
    }
}
