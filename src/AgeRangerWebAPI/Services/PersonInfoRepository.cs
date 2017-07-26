using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgeRangerWebAPI.Entities;
using AgeRangerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgeRangerWebAPI.Services
{
    /*******PersonInfoRepository************/
    public class PersonInfoRepository : IPersonInfoRepository
    {
        private PersonInfoContext _context;

        public PersonInfoRepository(PersonInfoContext context)
        {
            _context = context;
        }

        /*Returns a list of Person ordered by firstName , for populating the table */
        public IList<PersonDto> GetPersons()
        {
            List<PersonDto> personDtoList = new List<PersonDto>();

            var personList = _context.Persons.ToList();
            var ageGroupList = _context.AgeGroups.ToList();

            personDtoList = CommonServices.GetPersonsDto(personList, ageGroupList);

            return personDtoList.OrderBy(p => p.FirstName).ToList();
        }

        /*Returns a person or a list ordered by firstName , for the sever side search */
        public IList<PersonDto> GetPersons(PersonSearch search)
        {
            List<PersonDto> personDtoList = new List<PersonDto>();

            var personList = _context.Persons.ToList();
            var ageGroupList = _context.AgeGroups.ToList();

            personDtoList = CommonServices.GetPersonsDto(personList, ageGroupList);

            var personSeachList = personDtoList.Where(p => (string.IsNullOrEmpty(search.FirstName) ? true : p.FirstName.ToLower().Contains(search.FirstName.ToLower())) &&
                                          (string.IsNullOrEmpty(search.LastName) ? true : p.LastName.ToLower().Contains(search.LastName.ToLower())))
                                    .OrderBy(p => p.FirstName).ToList();
            return personSeachList;
        }

        /*Returns a single person for a given id */
        public PersonDto GetPerson(int Id)
        {
            if (Id < 1)
            {
                PersonDto personDtoObjNull = new PersonDto() { Id = 0, FirstName = "", LastName = "", Age = 0, AgeGroup = "" };
                return personDtoObjNull;
            }
            var personObj = _context.Persons.Where(c => c.Id == Id).FirstOrDefault();
            var ageGroupList = _context.AgeGroups.ToList();

            PersonDto personDtoObj = new PersonDto()
            {
                Id = personObj.Id,
                FirstName = personObj.FirstName,
                LastName = personObj.LastName,
                Age = personObj.Age,
                AgeGroup = CommonServices.AgeGroupDescription(personObj.Age, ageGroupList)
            };
            return personDtoObj;
        }

        /*Save  a single record for person based on 'Add or Update' */
        public bool SavePerson(Person person, String action)
        {
            //if (person.Age < 0 )
            //{
            //    person.Age = 0;
            //}

            //if (person.Age > 2147483646)
            //{
            //    person.Age = 2147483646;
            //}

            if (action == "insert")
            {
                _context.Entry(person).State = EntityState.Added;
            }
            else
            {
                _context.Entry(person).State = EntityState.Modified;
            }
            return (_context.SaveChanges() >= 0);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        /*Deletes  a single record for person for a given Id */
        public void DeletePerson(int id)
        {
            var personObj = _context.Persons.Where(c => c.Id == id).FirstOrDefault();

            _context.Persons.Remove(personObj);
            _context.Entry(personObj).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
