using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgeRangerWebAPI.Models;

namespace AgeRangerWebAPI.Entities
{
    public class PersonInfoContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<AgeGroup> AgeGroups { get; set; }

        public PersonInfoContext(DbContextOptions<PersonInfoContext> options)
           : base(options)
        {
            Database.Migrate();
        }
    }
}
