using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARYEFCORE.Models
{
    public class Category
    {
        [Key]
        public int CID { get; set; }
        public string CName { get; set; }
        public int NumberOfBooks { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
