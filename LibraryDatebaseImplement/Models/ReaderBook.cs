using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.Enums;

namespace LibraryDatebaseImplement.Models
{
    public class ReaderBook
    {
        public int? Id { get; set; }

        public int ReaderId { get; set; }

        public int BookId { get; set; }

        public int BookBranchId { get; set; }

        public DateTime DateOut { get; set; }

        public DateTime? DateIn { get; set; }

        public BookBranchStatus Status { get; set; }

        public virtual Reader Reader { get; set; }

        public virtual Book Book { get; set; }

        public virtual BookBranch BookBranch { get; set; }
    }
}
