using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using DatingApp.API.DTO;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDatingRepository _repo;

        public MessagesController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name="GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    return Unauthorized(); 

            var messageFromRepo = await _repo.GetMessage(id);

            if(messageFromRepo == null)
                return NotFound();

            return Ok(messageFromRepo); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDTO msgCrestionDTO)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                        return Unauthorized();  

            msgCrestionDTO.SenderId = userId;

            var recipient = await _repo.GetUser(msgCrestionDTO.RecipientId);

            if(recipient == null)
                return BadRequest("User not Found");  
            
            var message = _mapper.Map<Message>(msgCrestionDTO);
            
            _repo.Add(message);

            var messageToReturn = _mapper.Map<MessageForCreationDTO>(message);

            if(await _repo.SaveAll())
                return CreatedAtRoute("GetMessage", new {userId, id = message.Id}, messageToReturn);

            throw new Exception("Creatin the message failed");
        }
    }
}