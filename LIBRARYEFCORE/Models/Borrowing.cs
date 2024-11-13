using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARYEFCORE.Models
{
    public class Borrowing
    {
        [Key]
        public int BorID { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime PredictedReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public int Rating { get; set; }
        public bool IsReturned { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}