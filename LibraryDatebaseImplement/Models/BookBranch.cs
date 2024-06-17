using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatebaseImplement.Models
{
    public class BookBranch
    {
        public int? Id { get; set; }

        public int BranchId { get; set; }

        public int BookId { get; set; }

        public int Amount { get; set; }

        public int AmountInStock { get; set; }

        public DateTime DateOf { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Book Book { get; set; }
    }
}
