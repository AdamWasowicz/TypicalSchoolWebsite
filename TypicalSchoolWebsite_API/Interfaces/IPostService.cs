using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.Post;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IPostService
    {
        public int CreatePost(CreatePostDTO dto, ClaimsPrincipal userClaims);
        public List<PostDTO> GetAllPosts();
    }
}
