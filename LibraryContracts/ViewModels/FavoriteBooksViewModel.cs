using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.ViewModels
{
    public class FavoriteBooksViewModel
    {
        public int? Id { get; set; }

        public int ReaderId { get; set; }

        public int BookId { get; set; }
    }
}
