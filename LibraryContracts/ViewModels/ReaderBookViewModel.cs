using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.Enums;

namespace LibraryContracts.ViewModels
{
    public class ReaderBookViewModel
    {
        public int? Id { get; set; }

        public int BookBranchId { get; set; }

        public int ReaderId { get; set; }

        public int BookId { get; set; }

        public DateTime DateOut { get; set; }

        public DateTime? DateIn { get; set; }

        public BookBranchStatus Status { get; set; }
    }
}
