using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgeRangerWebAPI.Entities;

namespace AgeRangerWebAPI
{
    public static class AgeRangerInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this PersonInfoContext context)
        {
            if (context.AgeGroups.Any())
            {
                return;
            }

            // init seed data
            var AgeGroups = new List<AgeGroup>()
            {
                new AgeGroup() { MinAge =0,MaxAge=2,Description = "Toddler" },
                new AgeGroup() { MinAge =2,MaxAge=14,Description = "Child" },
                new AgeGroup() { MinAge =14,MaxAge=20,Description = "Teenager" },
                new AgeGroup() { MinAge =20,MaxAge=25,Description = "Early twenties" },
                new AgeGroup() { MinAge =25,MaxAge=30,Description = "Almost thirty" },
                new AgeGroup() { MinAge =30,MaxAge=50,Description = "Very adult" },
                new AgeGroup() { MinAge =50,MaxAge=70,Description = "Kinda old" },
                new AgeGroup() { MinAge =70,MaxAge=99,Description = "Old" },
                new AgeGroup() { MinAge =99,MaxAge=110,Description = "Very Old" },
                new AgeGroup() { MinAge =110,MaxAge=199,Description = "Crazy ancient" },
                new AgeGroup() { MinAge =199,MaxAge=4999,Description = "Vampire" },
                new AgeGroup() { MinAge =4999,MaxAge=2147483647,Description = "Kauri tree" },
            };

            context.AgeGroups.AddRange(AgeGroups);
            context.SaveChanges();
        }
    }
}
