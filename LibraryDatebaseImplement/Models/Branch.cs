using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDatebaseImplement.Models
{
    public class Branch
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string Schedule { get; set; }

        public double Сoordinate1 { get; set; }

        public double Сoordinate2 { get; set; }

        [ForeignKey("BranchId")]
        public virtual List<Librarian> Librarians { get; set; }

        [ForeignKey("BranchId")]
        public virtual List<BookBranch> BookBranches { get; set; }
    }
}
