using Microsoft.AspNetCore.Http;
using System;

namespace TypicalSchoolWebsite_FileStorageService.Interfaces
{
    public interface IFileStorageService
    {
        string CreateImage(IFormFile file);
        int DeleteImage(string hashedName);
        Tuple<byte[], string> ReturnImage(string hashedName);
    }
}