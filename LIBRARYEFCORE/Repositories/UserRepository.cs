using LIBRARYEFCORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARYEFCORE.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.UName == name);
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateByName(string name, User updatedUser)
        {
            var user = GetByName(name);
            if (user != null)
            {
                user.UName = updatedUser.UName;
                user.Gender = updatedUser.Gender;
                user.Passcode = updatedUser.Passcode;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public int CountByGender(string gender)
        {
            return _context.Users.Count(u => u.Gender == gender);
        }
    }
}
