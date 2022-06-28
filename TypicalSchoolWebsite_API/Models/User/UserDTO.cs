using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;


namespace TypicalSchoolWebsite_API.Models.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Surname { get; set; }

        public char? Gender { get; set; }

        public bool IsActive { get; set; }

        public bool IsSuspended { get; set; }


        //Role
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Entities.Role Role { get; set; }
    }
}
