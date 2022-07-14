using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.Post;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IPostService
    {
        Task<int> CreatePost(CreatePostDTO dto, ClaimsPrincipal userClaims);
        int DeletePostByAccessName(string accessName, ClaimsPrincipal userClaims);
        int DeletePostById(int id, ClaimsPrincipal userClaims);
        PostDTO EditPostByAccessName(EditPostDTO dto, ClaimsPrincipal userClaims);
        PostDTO EditPostById(EditPostDTO dto, ClaimsPrincipal userClaims);
        List<PostDTO> GetAllPosts();
        PostDTO GetPostByAccessName(string accessName);
        PostDTO GetPostById(int id);
        PostQueryResultDTO GetPostsUsingQuery(PostQueryDTO dto);
    }
}