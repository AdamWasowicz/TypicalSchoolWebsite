using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API
{
    public class TSW_MappingProfile : Profile
    {
        public TSW_MappingProfile()
        {
            //User
            CreateMap<User, UserDTO>();


            //Post
        }
    }
}
