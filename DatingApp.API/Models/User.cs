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
        public DateTime Dob { get; set; } 
        public DateTime AccCreated { get; set; }    
        public DateTime LastSeen { get; set; }  

        public ICollection<Photo> Photos { get; set; }

    }
}