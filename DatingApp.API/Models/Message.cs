using System;

namespace DatingApp.API.Models
{
    public class Message
    {
        public int Id { get; set; } 
        public int SenderId { get; set; }   
        public User Sender { get; set; } 
        public User Recipient { get; set; } 
        public int RecipientId { get; set; }  
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? MessgaeReadTime { get; set; }
        public DateTime? MessageSentTime { get; set; }
        public bool RecipientDeleted { get; set; }
        public bool SenderDeleted { get; set; }
    }
}