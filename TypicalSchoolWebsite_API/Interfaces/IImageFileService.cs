using System;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Models.ImageFile;

namespace TypicalSchoolWebsite_API.Interfaces
{
    public interface IImageFileService
    {
        Task<int> CheckConnection();
        Task<string> CreateImage(CreateImageFileDTO dto);
        Task<Tuple<byte[], string>> GetImage(string storageName);
    }
}