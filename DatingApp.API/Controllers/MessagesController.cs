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


        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery]MessageParams messageParams)
        { 
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized(); 
            messageParams.UserId = userId;
            
            var messageFromRepo = await _repo.GetMessagesForUser(messageParams);
            var messages = _mapper.Map<IEnumerable<MessageToReturnDTO>>(messageFromRepo);
            Response.AddPagination(messageFromRepo.CurrentPage, messageFromRepo.PageSize, messageFromRepo.TotalCount, messageFromRepo.TotalPages);
            return Ok(messages);
        }



         [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessagesThread(int userId, int recipientId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized(); 
            var messageFromRepo = await _repo.GetMessagesThread(userId, recipientId);
            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDTO>>(messageFromRepo);
            return Ok(messageThread);

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

            throw new Exception("Message creation failed");
        }

        [HttpPost("{id}/read")]
      public async Task<IActionResult> MarkMessageAsRead(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                        return Unauthorized();  
            
            var message = await _repo.GetMessage(id);

            if(message.RecipientId != userId)
                return Unauthorized();
            
            message.IsRead = true;
            message.MessgaeReadTime = DateTime.Now;
            await _repo.SaveAll();
            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteMessage(int id, int userId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                        return Unauthorized(); 

            var messageFromRepo = await _repo.GetMessage(id);

            if(messageFromRepo.SenderId == userId)
                messageFromRepo.SenderDeleted = true;

            if(messageFromRepo.RecipientId == userId)
                messageFromRepo.RecipientDeleted = true;
            
            if(messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted)
                _repo.Delete(messageFromRepo);
            
            if(await _repo.SaveAll())
                return NoContent();
            
            throw new Exception("Error Deleting the messages");

        }

    }

      
}