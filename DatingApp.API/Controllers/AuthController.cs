using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTO;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;    
        }

  
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserForSignUpDTO registerDTO )
        {
       
            registerDTO.Name = registerDTO.Name.ToLower();
          
            if(await _repo.CheckUser(registerDTO.Name))
                return BadRequest("Username Already Exists");

            var userToCreate = new User
            {
                Name = registerDTO.Name
            };

            var createdUser = await _repo.SignUp(userToCreate, registerDTO.Password);
            return StatusCode(201);
        }

            
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(UserForSignInDTO signInDTO )
        {
            var userFromRepo = await _repo.LogIn(signInDTO.Name.ToLower(), signInDTO.Password);

            if(userFromRepo == null)
                return Unauthorized();

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds 
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });

        } 
    }
}