using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DatingApp.API.Helpers;
using System;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context) //To Access DB
        {
            this._context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoOfUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync( p => p.Id == id);
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(UserParams userParams)
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();

            users = (users.Where(u => u.Id != userParams.UserId)).ToList();

            users = (users.Where(u => u.Gender != userParams.Gender)).ToList();

            if(userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                 var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                 var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                 users = (users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob)).ToList();
            }
            
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() >0;
        }
    }
}