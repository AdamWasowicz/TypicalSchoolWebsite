using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Models.PostLog;
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
            CreateMap<Post, PostDTO>()
                .ForMember(m => m.ImageStorageName, c => c.MapFrom(s => s.ImageFile.StorageName));
            
            CreateMap<CreatePostDTO, Post>();
            CreateMap<EditPostDTO, Post>();


            //Role
            CreateMap<Role, RoleDTO>();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<EditRoleDTO, Role>();


            //PostLog
            CreateMap<CreatePostLogDTO, PostLog>()
                //.ForMember(d => d.Operation, s => s.MapFrom(c => c.Operation))
                .ForMember(d => d.PreviousTitle, s => s.MapFrom(c => c.PreviousState.Title))
                .ForMember(d => d.CurrentTitle, s => s.MapFrom(c => c.CurrentState.Title))
                .ForMember(d => d.PreviousTextContent, s => s.MapFrom(c => c.PreviousState.TextContent))
                .ForMember(d => d.CurrentTextContent, s => s.MapFrom(c => c.CurrentState.TextContent))
                .ForMember(d => d.PreviousIsActive, s => s.MapFrom(c => c.PreviousState.IsActive))
                .ForMember(d => d.CurrentIsActive, s => s.MapFrom(c => c.CurrentState.IsActive))
                .ForMember(d => d.PostId, s => s.MapFrom(c => c.CurrentState.Id));

            CreateMap<PostLog, PostLogDTO>();
        }
    }
}
