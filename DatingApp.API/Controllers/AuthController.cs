using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTO;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserForSignUpDTO userForRegisterDTO )
        {
            userForRegisterDTO.Name = userForRegisterDTO.Name.ToLower();
            if(await _repo.CheckUser(userForRegisterDTO.Name))
                return BadRequest("Username Already Exists");
            var userToCreate = new User
            {
                Name = userForRegisterDTO.Name
            };

            var createdUser = await _repo.SignUp(userToCreate, userForRegisterDTO.Password);
            return StatusCode(201);
        }

    }
}