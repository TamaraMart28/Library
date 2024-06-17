using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.BindingModels
{
    public class BookBranchBindingModel
    {
        public int? Id { get; set; }

        public int BranchId { get; set; }

        public int BookId { get; set; }

        public int Amount { get; set; }

        public int AmountInStock { get; set; }

        public DateTime DateOf { get; set; }
    }
}
