using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.DTO
{
    public class UserForDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } //Username
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FullName { get; set; }    
        public string Email { get; set; }   
        public string Gender { get; set; }  
        public int Age { get; set; } 
        public DateTime AccCreated { get; set; }    
        public DateTime LastSeen { get; set; }  
        public string CurrentCity { get; set; }
        public string RaisedCity { get; set; }
        public string Religion { get; set; }
        public string Bio { get; set; }
        public string RelationshipStatus { get; set; }
        public string LookingFor { get; set; }
        public string InterestedIn { get; set; }
        public string PhotoURL { get; set; }
        public ICollection<PhotoForDetailsDTO> Photos { get; set; }
    }
}