using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TypicalSchoolWebsite_API.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public int AccessLevel { get; set; }
    }
}
