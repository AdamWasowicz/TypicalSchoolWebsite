﻿namespace TypicalSchoolWebsite_API.Models.Role
{
    public class EditRoleDTO
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public int AccessLevel { get; set; }
    }
}
