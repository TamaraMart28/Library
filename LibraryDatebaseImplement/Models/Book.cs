using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDatebaseImplement.Models
{
    public class Book
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Genres { get; set; }
       
        public string Annotation { get; set; }

        public int Year { get; set; }

        public string ImageName { get; set; }

        public string ImagePath { get; set; }

        [ForeignKey("BookId")]
        public virtual List<FavoriteBooks> FavoriteBooks { get; set; }

        [ForeignKey("BookId")]
        public virtual List<BookBranch> BookBranches { get; set; }

        [ForeignKey("BookId")]
        public virtual List<ReaderBook> ReaderBooks { get; set; }
    }
}
