using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.DTO
{
    public class PhotoUploadDTO
    {
        public string Url { get; set; } 
        public IFormFile File { get; set; }
        public string Description { get; set; }     
        public DateTime DateAdded { get; set; } 
        public string PublicId { get; set; }

        public PhotoUploadDTO()
        {
            DateAdded = DateTime.Now;
        }
    }
}