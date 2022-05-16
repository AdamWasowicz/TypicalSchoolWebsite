using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;

namespace TypicalSchoolWebsite_API.Services
{
    public class UserService
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



    }
}
