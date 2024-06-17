using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.ViewModels
{
    public class ReportBooksViewModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
    }
}
