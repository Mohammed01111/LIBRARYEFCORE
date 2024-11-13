using LIBRARYEFCORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARYEFCORE.Repositories
{
    public class AdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }

        public Admin GetByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(a => a.Email == email);
        }

        public void Insert(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateByEmail(string email, Admin updatedAdmin)
        {
            var admin = GetByEmail(email);
            if (admin != null)
            {
                admin.AName = updatedAdmin.AName;
                admin.Password = updatedAdmin.Password;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }
    }
}
