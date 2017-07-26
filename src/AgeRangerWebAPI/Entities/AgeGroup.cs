using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgeRangerWebAPI.Entities
{
    public class AgeGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ? MinAge { get; set; }

        public int ? MaxAge { get; set; }
        
        public string Description { get; set; }
    }
}
