using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgeRangerWebAPI.Entities;
using AgeRangerWebAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace AgeRangerWebAPI.Services
{
    /******   CommonServices  *******
     * A static class for the Common functionalities . Primary purpose of the class is to avoid 
     * the code duplication and add more consistency to the code */
    public static class CommonServices
    {
        /*Returns a list of Person Dto objects */
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

        /*Returns age group description for a given age of the person */
        public static string AgeGroupDescription (int age , List<AgeGroup> ageGroupList)
        {
            //int indexMinAgeNull = ageGroupList.FindIndex(a => a.MinAge == null || a.MinAge == 0);
            //int indexMaxAgeNull = ageGroupList.FindIndex(a => a.MaxAge == null  || a.MaxAge == 2147483646);

            if (ageGroupList[0].MinAge == null || ageGroupList[11].MaxAge == null)
            {
                ageGroupList[0].MinAge = 0;
                ageGroupList[11].MaxAge = 2147483646;
            }

            return ageGroupList.Where(a => a.MinAge <= age && a.MaxAge > age).FirstOrDefault().Description;
        }
    }
}
