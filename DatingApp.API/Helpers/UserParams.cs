namespace DatingApp.API.Helpers
{
    public class UserParams
    {
        public int UserId { get; set; } 
        public string InterestedIn { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 99;
    }
}