using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTO
{
    public class UserForSignUpDTO
    {

        [Required]
        public string Name { get; set; }  

        [Required]
        [StringLength(10, MinimumLength=5, ErrorMessage="Your password should be between 5-10 charcacters long")]
        public string Password { get; set; }
    }
}