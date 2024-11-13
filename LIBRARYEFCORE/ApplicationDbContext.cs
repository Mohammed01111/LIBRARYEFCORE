using LIBRARYEFCORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LIBRARYEFCORE
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(" Data Source=DESKTOP-VFQRVL8\\MSSQLSERVER02; Initial Catalog=LIBRARYEFCORE; Integrated Security=true; TrustServerCertificate=True ");
        }

    }   

}
