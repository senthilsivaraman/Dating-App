using System;
using DatingApp.API.Models;

namespace DatingApp.API.DTO
{
    public class MessageToReturnDTO
    {
        public int Id { get; set; } 
        public int SenderId { get; set; }   
        public string SenderFullName { get; set; }
        public string SenderPhotoUrl { get; set; } 
        public int RecipientId { get; set; }  
        public string RecipientFullName { get; set; } 
        public string RecipientPhotoUrl { get; set; } 
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? MessgaeReadTime { get; set; }
        public DateTime? MessageSentTime { get; set; }
    }
}