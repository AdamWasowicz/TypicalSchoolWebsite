using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Models.Account
{
    public class ChangePasswordDTO
    {
        public string UserName { get; set; }

        public string PreviousPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
