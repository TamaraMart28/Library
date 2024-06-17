using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.ViewModels
{
    public class ReaderViewModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public Dictionary<int, string> FavoriteBooks { get; set; }

        public Dictionary<int, string> ReaderBooks { get; set; }
    }
}
