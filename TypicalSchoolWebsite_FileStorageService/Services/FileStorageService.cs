using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using TypicalSchoolWebsite_FileStorageService.Interfaces;

namespace TypicalSchoolWebsite_FileStorageService.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string fileStoragePath = "FileStorage";
        private readonly string imageSubCatalog = "Images";


        public FileStorageService()
        {

        }


        private string HashName(IFormFile file)
        {
            string toBeHashedString = file.FileName.Split('.')[file.FileName.Split('.').Length - 1] + DateTime.Now.ToString();
            using (var sha = new SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(toBeHashedString);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }


        public string CreateImage(IFormFile file)
        {
            string extension = "";

            if (file != null && file.Length > 0)
            {
                extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var rootPath = Directory.GetCurrentDirectory();
                var hashedName = HashName(file);

                var fullPath = $"{rootPath}/{fileStoragePath}/{imageSubCatalog}/{hashedName}.{extension}";


                try
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    return "";
                }


                return $"{hashedName}.{extension}";
            }

            return "";
        }


        public int DeleteImage(string hashedName)
        {
            var pathToCatalog = $"{Directory.GetCurrentDirectory()}/{fileStoragePath}/{imageSubCatalog}/";

            if (!File.Exists(pathToCatalog + hashedName))
                return -1;
            else
            {
                File.Delete(pathToCatalog + hashedName);

                if (File.Exists(pathToCatalog + hashedName))
                    return -2;
                else
                    return 0;
            }
        }


        public Tuple<byte[], string> ReturnImage(string hashedName)
        {
            var pathToCatalog = $"{Directory.GetCurrentDirectory()}/{fileStoragePath}/{imageSubCatalog}/";

            if (!File.Exists(pathToCatalog + hashedName))
                return new Tuple<byte[], string>(null, "error");
            else
            {
                var retTuple = new Tuple<byte[], string>(File.ReadAllBytes(pathToCatalog + hashedName), hashedName);
                return retTuple;
            }
        }
    }
}
