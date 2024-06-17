using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDatebaseImplement.Models
{
    public class Reader
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        [ForeignKey("ReaderId")]
        public virtual List<FavoriteBooks> FavoriteBooks { get; set; }

        [ForeignKey("ReaderId")]
        public virtual List<ReaderBook> ReaderBooks { get; set; }
    }
}
