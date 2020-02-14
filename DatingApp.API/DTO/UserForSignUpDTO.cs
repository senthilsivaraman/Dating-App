using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTO
{
    public class UserForSignUpDTO
    {
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string FullName { get; set; }    
        [Required]
        public string Email { get; set; }
        [Required]
        public string InterestedIn { get; set; }
        [Required]
        [StringLength(10, MinimumLength=5, ErrorMessage="Your password should be between 5-10 charcacters long")]
        public string Password { get; set; }
        public DateTime AccCreated { get; set; }
        public DateTime LastSeen { get; set; }

        public UserForSignUpDTO()
        {
            AccCreated = DateTime.Now;
            LastSeen = DateTime.Now;
        }
    }
}