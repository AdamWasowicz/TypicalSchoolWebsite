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
using TypicalSchoolWebsite_API.Models.Role;

namespace TypicalSchoolWebsite_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;


        public RoleService(
            TSW_DbContext dbContext,
            IMapper mapper,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }


        public int CreateRole(CreateRoleDTO dto, ClaimsPrincipal userClaims)
        {
            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, null,
                new ResourceOperationRequirement(ResourceOperation.Create));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();

            var newRole = new Role()
            {
                RoleName = dto.RoleName,
                Description = dto.Description,
                AccessLevel = dto.AccessLevel
            };

            _dbContext.Roles.Add(newRole);
            _dbContext.SaveChanges();

            return newRole.Id;
        }


        public List<RoleDTO> GetAllRoles()
        {
            var roles = _dbContext.Roles
                .ToList();

            if (roles.Count == 0)
                throw new NotFoundException();

            var rolesDTO = _mapper.Map<List<RoleDTO>>(roles);

            return rolesDTO;
        }


        public RoleDTO GetRoleById(int id)
        {
            var role = _dbContext.Roles
                .Where(r => r.Id == id)
                    .FirstOrDefault();

            if (role == null)
                throw new NotFoundException();

            var roleDTO = _mapper.Map<RoleDTO>(role);

            return roleDTO;
        }


        public int DeleteRoleById(int id, ClaimsPrincipal userClaims)
        {
            var role = _dbContext.Roles
                .Where(r => r.Id == id)
                    .FirstOrDefault();

            if (role == null)
                throw new NotFoundException();

            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, role,
                new ResourceOperationRequirement(ResourceOperation.Delete));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();

            _dbContext.Roles.Remove(role);
            _dbContext.SaveChanges();

            //Check if deleted
            var deleted = _dbContext.Roles
                .Where(r => r.Id == id)
                    .Any();

            if (!deleted)
                return -1;


            return 0;
        }


        public RoleDTO EditRoleById(EditRoleDTO dto, ClaimsPrincipal userClaims)
        {
            var role = _dbContext.Roles
                .Where(r => r.Id == dto.Id)
                    .FirstOrDefault();


            //Authorization
            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, role,
                new ResourceOperationRequirement(ResourceOperation.Update));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new BadAuthorizationExeption();


            //Changes
            role.RoleName = dto.RoleName;
            role.Description = dto.Description;
            role.AccessLevel = dto.AccessLevel;


            _dbContext.SaveChanges();


            var roleDTO = _mapper.Map<RoleDTO>(role);
            return roleDTO;
        }
    }
}
