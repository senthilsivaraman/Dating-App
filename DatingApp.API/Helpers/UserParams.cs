namespace DatingApp.API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        private int PageNumber { get; set; } = 1; 
        private int pageSize = 10;  
        private int PageSize
        {
            get {return pageSize;}
            set {pageSize = (value > MaxPageSize) ? MaxPageSize : value ;}
        } 
         
        public int UserId { get; set; } 
        public string InterestedIn { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 99;

        public bool Likees { get; set; } = false;
        public bool Likers { get; set; } = false;
        
    }
}