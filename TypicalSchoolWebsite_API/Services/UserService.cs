using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Authorization.Requirements;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Exceptions;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.User;

namespace TypicalSchoolWebsite_API.Services
{
    public class UserService : IUserService
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;


        public UserService(
            TSW_DbContext dbContext,
            IMapper mapper,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }


        private User GetUserEntityById(int id)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == id)
                    .FirstOrDefault();

            if (user == null)
                throw new NotFoundException();

            return user;
        }

        private User GetUserEntityByUserName(string userName)
        {
            var user = _dbContext.Users
                .Where(u => u.UserName == userName)
                    .FirstOrDefault();

            if (user == null)
                throw new NotFoundException();

            return user;
        }

        private int DeleteUser(User user, ClaimsPrincipal userClaims)
        {
            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, user,
                new ResourceOperationRequirement(ResourceOperation.Delete));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();


            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();


            //Check if deleted
            var deleted = _dbContext.Users
                .Where(p => p.Id == user.Id)
                    .Any();

            if (!deleted)
                return -1;


            return 0;
        }

        private UserDTO EditUser(User user, EditUserDTO dto, ClaimsPrincipal userClaims)
        {
            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, user,
                new ResourceOperationRequirement(ResourceOperation.Update));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();


            //Changes
            user.UserName = dto.UserName;
            user.FirstName = dto.FirstName;
            user.SecondName = dto.SecondName;
            user.Surname = dto.Surname;
            user.Gender = dto.Gender;
            user.LastAccountModificationDate = DateTime.Now;

            _dbContext.SaveChanges();

            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }



        public List<UserDTO> GetAllUsers()
        {
            var users = _dbContext.Users
                .Include(r => r.Role)
                    .ToList();

            if (users.Count == 0)
                throw new NotFoundException("No records found");

            var usersDTO = _mapper.Map<List<UserDTO>>(users);

            return usersDTO;
        }


        public UserDTO GetUserById(int id)
        {
            var user = GetUserEntityById(id);

            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }


        public UserDTO GetUserByUserName(string userName)
        {
            var user = GetUserEntityByUserName(userName);

            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }


        public int DeleteUserById(int id, ClaimsPrincipal userClaims)
        {
            var user = GetUserEntityById(id);

            return DeleteUser(user, userClaims);
        }


        public int DeleteUserByUserName(string userName, ClaimsPrincipal userClaims)
        {
            var user = GetUserEntityByUserName(userName);

            return DeleteUser(user, userClaims);
        }


        public UserDTO EditUserById(EditUserDTO dto, ClaimsPrincipal userClaims)
        {
            var user = GetUserEntityById(dto.Id);

            return EditUser(user, dto, userClaims);
        }


        public UserDTO EditUserByUserName(EditUserDTO dto, ClaimsPrincipal userClaims)
        {
            var user = GetUserEntityByUserName(dto.UserName);

            return EditUser(user, dto, userClaims);
        }
    }
}
