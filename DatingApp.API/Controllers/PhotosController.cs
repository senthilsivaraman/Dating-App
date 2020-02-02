using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.DTO;
using CloudinaryDotNet;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.Extensions.Options;
using CloudinaryDotNet.Actions;
using System.Linq;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController (IDatingRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)  
        {

            _cloudinaryConfig = cloudinaryConfig;
             _repo = repo;
            _mapper = mapper;

           Account acc = new Account(
               _cloudinaryConfig.Value.CloudName,
               _cloudinaryConfig.Value.ApiKey,
               _cloudinaryConfig.Value.ApiSecret
           );

           _cloudinary = new Cloudinary(acc);
        } 

        [HttpGet("{id}", Name ="GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);
            var photo = _mapper.Map<DisplayPhotoDto>(photoFromRepo);
            return Ok(photo);
        }


        [HttpPost]
        public async Task<IActionResult> AddMultiplePhotos(int userId, PhotoUploadDTO photoUploadDTO)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();   

            var userFromRepo = await _repo.GetUser(userId);     
            var file = photoUploadDTO.File;
            var uploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoUploadDTO.Url = uploadResult.Uri.ToString();
            photoUploadDTO.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoUploadDTO);

            //Setting first upload as main photo
            if(!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain =true;
            
            userFromRepo.Photos.Add(photo);

            if(await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<DisplayPhotoDto>(photo);
                return CreatedAtRoute("GetPhoto", new {userId = userId, id = photo.Id}, photoToReturn);
            }
            return BadRequest("Could not add the photo");
        }
    }
}