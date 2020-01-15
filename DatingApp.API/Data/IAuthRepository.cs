using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        Task<User> SignUp (User user, string password);     
        Task<User> LogIn (string username, string password);   
        Task<bool> CheckUser (string username);
    }
}