using LIBRARYEFCORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARYEFCORE.Repositories
{
    public class BorrowingRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Borrowing> GetAll()
        {
            return _context.Borrowings.Include(b => b.User).Include(b => b.Book).ToList();
        }

        public Borrowing GetById(int id)
        {
            return _context.Borrowings.Include(b => b.User).Include(b => b.Book)
                                      .FirstOrDefault(b => b.BorID == id);
        }
        public Book GetByName(string name)
        {
            return _context.Books.FirstOrDefault(b => b.BName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public Borrowing GetByBookIdAndUserId(int bookId, int userId)
        {
            return _context.Borrowings.FirstOrDefault(b => b.BookId == bookId && b.UserId == userId && !b.IsReturned);
        }
        
        public void Insert(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }

        public void UpdateById(int id, Borrowing updatedBorrowing)
        {
            var borrowing = GetById(id);
            if (borrowing != null)
            {
                borrowing.BorrowingDate = updatedBorrowing.BorrowingDate;
                borrowing.PredictedReturnDate = updatedBorrowing.PredictedReturnDate;
                borrowing.ActualReturnDate = updatedBorrowing.ActualReturnDate;
                borrowing.Rating = updatedBorrowing.Rating;
                borrowing.IsReturned = updatedBorrowing.IsReturned;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var borrowing = _context.Borrowings.Find(id);
            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
                _context.SaveChanges();
            }
        }

        public void ReturnBook(int borrowingId, DateTime actualReturnDate, int rating)
        {
            var borrowing = GetById(borrowingId);
            if (borrowing != null && !borrowing.IsReturned)
            {
                borrowing.ActualReturnDate = actualReturnDate;
                borrowing.Rating = rating;
                borrowing.IsReturned = true;

                // Update book's borrowed count
                var book = borrowing.Book;
                book.BorrowedCopies--;

                _context.SaveChanges();
            }
        }
    }

}
