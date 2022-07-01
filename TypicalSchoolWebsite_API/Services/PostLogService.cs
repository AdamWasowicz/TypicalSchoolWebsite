using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Authorization.Requirements;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Exceptions;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Models.PostLog;

namespace TypicalSchoolWebsite_API.Services
{
    public class PostLogService : IPostLogService
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;


        public PostLogService(
            TSW_DbContext dbContext,
            IMapper mapper,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }


        public int CreatePostLog(CreatePostLogDTO dto)
        {
            var postLog = _mapper.Map<PostLog>(dto);

            _dbContext.PostLogs.Add(postLog);
            _dbContext.SaveChanges();

            return postLog.Id;
        }


        public List<PostLogDTO> GetAllPostLog()
        {
            var listPostLog = _dbContext.PostLogs
                .ToList();

            if (listPostLog.Count == 0)
                throw new NotFoundException();

            var listPostLogDTO = _mapper.Map<List<PostLogDTO>>(listPostLog);

            return listPostLogDTO;
        }


        public PostLogDTO GetPostLogById(int id)
        {
            var postLog = _dbContext.PostLogs
                .Where(pl => pl.Id == id)
                    .FirstOrDefault();

            if (postLog == null)
                throw new NotFoundException();

            var postLogDTO = _mapper.Map<PostLogDTO>(postLog);

            return postLogDTO;
        }
    }
}
