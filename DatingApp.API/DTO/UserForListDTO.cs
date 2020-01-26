using System;

namespace DatingApp.API.DTO
{
    public class UserForListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } //Username
        public string FullName { get; set; }    
        public string Gender { get; set; }  
        public int Age { get; set; } 
        public DateTime AccCreated { get; set; }    
        public DateTime LastSeen { get; set; }  
        public string CurrentCity { get; set; }
        public string RaisedCity { get; set; }
        public string RelationshipStatus { get; set; }
        public string LookingFor { get; set; }
        public string PhotoUrl { get; set; }
    }
}