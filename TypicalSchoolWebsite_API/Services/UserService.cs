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


        //GetAllUsers
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


        //GetUserById
        public UserDTO GetUserById(int id)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == id)
                    .FirstOrDefault();

            if (user == null)
                throw new NotFoundException();

            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }


        //DeleteUserById
        public int DeleteUserById(int id, ClaimsPrincipal userClaims)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == id)
                    .FirstOrDefault();

            if (user == null)
                throw new NotFoundException();


            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, user,
                new ResourceOperationRequirement(ResourceOperation.Delete));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();


            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();


            //Check if deleted
            var deleted = _dbContext.Users
                .Where(p => p.Id == id)
                    .Any();

            if (!deleted)
                return -1;


            return 0;
        }


        //EditUserById
        public UserDTO EditUserById(EditUserDTO dto, ClaimsPrincipal userClaims)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == dto.Id)
                    .FirstOrDefault();


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
    }
}
