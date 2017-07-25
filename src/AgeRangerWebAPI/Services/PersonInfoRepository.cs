using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgeRangerWebAPI.Entities;
using AgeRangerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgeRangerWebAPI.Services
{
   public class PersonInfoRepository : IPersonInfoRepository
   {
      private PersonInfoContext _context;

      public PersonInfoRepository(PersonInfoContext context)
      {
         _context = context;
      }

      public IList<PersonDto> GetPersons()
      {

         List<PersonDto> personDtoList = new List<PersonDto>();

         var personList = _context.Persons.ToList();
         var ageGroupList = _context.AgeGroups.ToList();

         for (var i = 0; i < personList.Count; i++)
         {
            personDtoList.Add(new PersonDto()
            {
               Id = personList[i].Id,
               FirstName = personList[i].FirstName,
               LastName = personList[i].LastName,
               Age = personList[i].Age,
               AgeGroup = ageGroupList.Where(a => a.MinAge <= personList[i].Age && a.MaxAge > personList[i].Age).FirstOrDefault().Description
            });
         }

         return personDtoList.OrderBy(p => p.FirstName).ToList();
      }

      public IList<PersonDto> GetPersons(PersonSearch search)
      {

         List<PersonDto> personDtoList = new List<PersonDto>();

         var personList = _context.Persons.ToList();
         var ageGroupList = _context.AgeGroups.ToList();

         for (var i = 0; i < personList.Count; i++)
         {
            personDtoList.Add(new PersonDto()
            {
               Id = personList[i].Id,
               FirstName = personList[i].FirstName,
               LastName = personList[i].LastName,
               Age = personList[i].Age,
               AgeGroup = ageGroupList.Where(a => a.MinAge <= personList[i].Age && a.MaxAge > personList[i].Age).FirstOrDefault().Description
            });
         }

         var personSeachList = personDtoList.Where(p => (string.IsNullOrEmpty(search.FirstName) ? true : p.FirstName.ToLower().Contains(search.FirstName.ToLower())) &&
                                        (string.IsNullOrEmpty(search.LastName) ? true : p.LastName.ToLower().Contains(search.LastName.ToLower())))
                                  .OrderBy(p => p.FirstName).ToList();
         return personSeachList;
      }

      public PersonDto GetPerson(int Id)
      {
         if (Id < 1)
         {
            PersonDto personDtoObjNull = new PersonDto() { Id = 0,FirstName = "",LastName = "",Age = 0, AgeGroup = "" };
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
            AgeGroup = ageGroupList.Where(a => a.MinAge <= personObj.Age && a.MaxAge > personObj.Age).FirstOrDefault().Description
         };

         return personDtoObj;
      }

      public bool SavePerson(Person person, String action)
      {
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

      public void DeletePerson(int id)
      {
         var personObj = _context.Persons.Where(c => c.Id == id).FirstOrDefault();

         _context.Persons.Remove(personObj);

         _context.Entry(personObj).State = EntityState.Deleted;
         _context.SaveChanges();
         
      }
   }
}
