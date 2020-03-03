using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T:class;
         void Delete<T> (T entity) where T:class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams); // All Users
         Task<User> GetUser(int id); //Individual User
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoOfUser(int userId);
         Task<Like> GetLike(int userId, int recipientId);
         Task<Message> GetMessage(int id);
         Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
         Task<PagedList<Message>> GetMessagesThread(int userId, int recipientId);
    }
}