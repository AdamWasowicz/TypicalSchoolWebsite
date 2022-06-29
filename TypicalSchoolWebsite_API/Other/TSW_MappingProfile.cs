using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Models.Role;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API
{
    public class TSW_MappingProfile : Profile
    {
        public TSW_MappingProfile()
        {
            //User
            CreateMap<User, UserDTO>();
            CreateMap<EditUserDTO, User>();


            //Post
            CreateMap<Post, PostDTO>();
            CreateMap<CreatePostDTO, Post>();
            CreateMap<EditPostDTO, Post>();


            //Role
            CreateMap<Role, RoleDTO>();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<EditRoleDTO, Role>();
        }
    }
}
