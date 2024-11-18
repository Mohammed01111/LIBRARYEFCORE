using LIBRARYEFCORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARYEFCORE.Repositories
{
    public class BookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.Include(b => b.Category).ToList();
        }
        public Book GetById(int id)
        {
            return _context.Books.Include(b => b.Category)
                                 .FirstOrDefault(b => b.BID == id);
        }
        public Book GetByName(string name)
        {
            return _context.Books.Include(b => b.Category)
                                 .FirstOrDefault(b => b.BName == name);
        }

        public void Insert(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public void UpdateById(int id, Book updatedBook)
        {
            var book = GetById(id);
            if (book != null)
            {
                book.BName = updatedBook.BName;
                book.Author = updatedBook.Author;
                book.TotalCopies = updatedBook.TotalCopies;
                book.BorrowedCopies = updatedBook.BorrowedCopies;
                book.CopyPrice = updatedBook.CopyPrice;
                book.AllowedBorrowingPeriod = updatedBook.AllowedBorrowingPeriod;
                book.CID = updatedBook.CID;
                _context.SaveChanges();
            }
        }

        public void UpdateByName(string name, Book updatedBook)
        {
            var book = GetByName(name);
            if (book != null)
            {
                book.BName = updatedBook.BName;
                book.Author = updatedBook.Author;
                book.TotalCopies = updatedBook.TotalCopies;
                book.BorrowedCopies = updatedBook.BorrowedCopies;
                book.CopyPrice = updatedBook.CopyPrice;
                book.AllowedBorrowingPeriod = updatedBook.AllowedBorrowingPeriod;
                book.CID = updatedBook.CID;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public decimal GetTotalPrice()
        {
            return _context.Books.Sum(b => b.CopyPrice);
        }

        public decimal GetMaxPrice()
        {
            return _context.Books.Max(b => b.CopyPrice);
        }
        public IEnumerable<Book> GetAvailableBooks()
        {
            return _context.Books
                .Where(b => b.TotalCopies > b.BorrowedCopies)
                .ToList();
        }

        public int GetTotalBorrowedBooks()
        {
            return _context.Books.Sum(b => b.BorrowedCopies);
        }

        public int GetTotalBooksPerCategoryName(string categoryName)
        {
            return _context.Books
                           .Where(b => b.Category.CName == categoryName)
                           .Sum(b => b.TotalCopies);
        }
    }
}
