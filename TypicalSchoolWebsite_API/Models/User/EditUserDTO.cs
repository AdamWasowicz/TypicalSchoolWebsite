namespace TypicalSchoolWebsite_API.Models.User
{
    public class EditUserDTO
    {
        public int Id { get; set; } 

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Surname { get; set; }

        public char? Gender { get; set; }
    }
}
