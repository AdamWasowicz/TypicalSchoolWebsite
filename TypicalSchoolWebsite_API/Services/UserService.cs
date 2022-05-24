using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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



        public UserService(
            TSW_DbContext dbContext,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

    }
}
