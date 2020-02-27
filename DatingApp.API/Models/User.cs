using System;
using System.Collections.Generic;

namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } //Username
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FullName { get; set; }    
        public string Email { get; set; }   
        public string Gender { get; set; }  
        public DateTime DateOfBirth { get; set; } 
        public DateTime AccCreated { get; set; }    
        public DateTime LastSeen { get; set; }  
        public ICollection<Photo> Photos { get; set; }

        public string CurrentCity { get; set; }
        public string RaisedCity { get; set; }
        public string Religion { get; set; }
        public string Bio { get; set; }
        public string RelationshipStatus { get; set; }
        public string LookingFor { get; set; }
        public string InterestedIn { get; set; }

        public ICollection<Like> Likers { get; set; }
        public ICollection<Like> Likees { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

    }
}