using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.ViewModels
{
    public class BookViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Genres { get; set; }

        public string Annotation { get; set; }

        public int Year { get; set; }

        public string ImageName { get; set; }

        public string ImagePath { get; set; }

        public Dictionary<int, string> BookBranches { get; set; }

        public Dictionary<int, string> ReaderBooks { get; set; }
    }
}
