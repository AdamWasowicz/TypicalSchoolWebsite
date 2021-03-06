using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Authorization.Requirements;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Exceptions;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.ImageFile;
using TypicalSchoolWebsite_API.Models.Post;
using TypicalSchoolWebsite_API.Models.PostLog;


namespace TypicalSchoolWebsite_API.Services
{
    public class ImageFileService : IImageFileService
    {
        private readonly TSW_DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;


        public ImageFileService(
            TSW_DbContext dbContext,
            IMapper mapper,
            HttpClient httpClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpClient = httpClient;
        }


        //CreateFileImage
        public async Task<string> CreateImage(CreateImageFileDTO dto)
        {
            var url = Environment.GetEnvironmentVariable("FILESTORAGESERVICE_URL");
            if (url == null || url == "")
                //url = "http://localhost:29439";           //DEFAULT NO CONTAINER
            url = "http://localhost:8001";                  //DEFAULT CONTAINER

            var route = "fileStorage/createImageFile";
            //_httpClient.BaseAddress = new Uri(url);
            var file = dto.File;

            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            ByteArrayContent bytes = new ByteArrayContent(data);


            MultipartFormDataContent multiContent = new MultipartFormDataContent();
            multiContent.Add(bytes, "file", file.FileName);


            //Send
            var httpResponse = await _httpClient.PostAsync(url + "/" + route, multiContent);

            if (httpResponse.IsSuccessStatusCode)
            {
                var respone = await httpResponse.Content.ReadAsStringAsync();

                if (respone != null)
                    return respone;
                else
                    return "";
            }
            else
                return "";
        }


        public async Task<Tuple<byte[], string>> GetImage(string storageName)
        {
            var url = Environment.GetEnvironmentVariable("FILESTORAGESERVICE_URL");
            if (url == null)
                url = "http://localhost:29439";               //DEFAULT NO CONTAINER
                //url = "http://localhost:8001";                  //DEFAULT CONTAINER
            var route = $"/fileStorage/getImageFile/{storageName}";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url + route);

            var httpResponse = await _httpClient.SendAsync(httpRequestMessage);

            var content = httpResponse.Content;
            var contentStream = await content.ReadAsByteArrayAsync();
            return new Tuple<byte[], string>(contentStream, storageName);
        }


        //Test
        public async Task<int> CheckConnection()
        {
            var url = Environment.GetEnvironmentVariable("FILESTORAGESERVICE_URL");
            if (url == null)
                //url = "http://localhost:29439";               //DEFAULT NO CONTAINER
                url = "http://localhost:8001";                  //DEFAULT CONTAINER
            var route = "/fileStorage/testConnection";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url + route);

            var httpResponse = await _httpClient.SendAsync(httpRequestMessage);

            var content = httpResponse.Content;
            string jsonContent = content.ReadAsStringAsync().Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
