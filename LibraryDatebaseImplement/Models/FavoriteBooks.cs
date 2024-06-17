using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatebaseImplement.Models
{
    public class FavoriteBooks
    {
        public int Id { get; set; }

        public int ReaderId { get; set; }

        public int BookId { get; set; }

        public virtual Reader Reader { get; set; }

        public virtual Book Book { get; set; }
    }
}
