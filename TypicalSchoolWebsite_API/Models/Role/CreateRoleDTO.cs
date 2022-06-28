namespace TypicalSchoolWebsite_API.Models.Role
{
    public class CreateRoleDTO
    {
        public string RoleName { get; set; }

        public string Description { get; set; }

        public int AccessLevel { get; set; }
    }
}
