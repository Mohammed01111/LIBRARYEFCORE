using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARYEFCORE.Models
{
    public class User
    {
        [Key]
        public int UID { get; set; }
        public string UName { get; set; }
        public string Gender { get; set; }
        public string Passcode { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
    }
}
