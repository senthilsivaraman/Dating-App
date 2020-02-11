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
        public async Task<IActionResult> AddMultiplePhotos(int userId, [FromForm]PhotoUploadDTO photoUploadDTO)
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
            };
            return BadRequest("Could not add the photo");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();   

            var user = await _repo.GetUser(userId); // Checking the match of photoid belongs to userId from repo

            if(!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            // Getting Photo from repo
            var photoFromRepo = await _repo.GetPhoto(id);

            if(photoFromRepo.IsMain)
                return BadRequest("This is already your main photo");
            
            var currentMainPhoto = await _repo.GetMainPhotoOfUser(userId);
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;

            if(await _repo.SaveAll())
                return NoContent();
            return BadRequest("Could not Set this photo to main");
        }

        [HttpDelete("{id}")]
          public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();   
            
            var user = await _repo.GetUser(userId); // Checking the match of photoid belongs to userId from repo

            if(!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            // Getting Photo from repo
            var photoFromRepo = await _repo.GetPhoto(id);

            if(photoFromRepo.IsMain)
                return BadRequest("You caannot delete your main photo");

            //Deleting in Cloudinary
            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);
 
                var result = _cloudinary.Destroy(deleteParams);
 
                if (result.Result == "ok")
                {
                    _repo.Delete(photoFromRepo);
                }
            }
 
            if (photoFromRepo.PublicId == null){
                _repo.Delete(photoFromRepo);
            }
 
            if (await _repo.SaveAll())
                return Ok();
 
            return BadRequest("Failed to delete photo");
        }
    }
}