using System.Collections.Generic;
using TypicalSchoolWebsite_API.Models.PostLog;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IPostLogService
    {
        int CreatePostLog(CreatePostLogDTO dto);
        List<PostLogDTO> GetAllPostLog();
        PostLogDTO GetPostLogById(int id);
    }
}