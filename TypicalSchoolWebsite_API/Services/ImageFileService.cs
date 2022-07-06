using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            var route = "/fileStorage/createImageFile";
            var destination = url + route;
            url = "http://localhost:8001";
            _httpClient.BaseAddress = new Uri(url);


            //fileStreamContent
            var fileStreamContent = new StreamContent(dto.File.OpenReadStream());
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");


            //Content
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(fileStreamContent, name: "File", fileName: dto.File.Name);


            //Send
            var httpResponse = await _httpClient.PostAsync(route, multipartContent);

            if (httpResponse.IsSuccessStatusCode)
            {
                var respone = await httpResponse.Content.ReadAsStringAsync();
                if (respone != null)
                    return respone;
                else
                    return "No response";
            }
            else
                return "error";
        }


        //Test
        public async Task<int> CheckConnection()
        {
            var url = Environment.GetEnvironmentVariable("FILESTORAGESERVICE_URL");
            if (url == null)
                url = "http://localhost:8001";
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
