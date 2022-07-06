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
        public Task<int> CreatePost(CreatePostDTO dto, ClaimsPrincipal userClaims);
        public List<PostDTO> GetAllPosts();
        public PostDTO GetPostById(int id);
        public int DeletePostById(int id, ClaimsPrincipal userClaims);
        public PostDTO EditPostById(EditPostDTO dto, ClaimsPrincipal userClaims);
    }
}
