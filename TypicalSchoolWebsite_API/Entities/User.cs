using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }


        public DateTime RegisterDate { get; set; }
        
        public DateTime LastAccountModificationDate { get; set; }

        public DateTime LastLoginDate { get; set; }


        public bool IsActive { get; set; }

        public bool IsSuspended { get; set; }

        public string PasswordHash { get; set; }


        //Foreign
        //Role 
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
