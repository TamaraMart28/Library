using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.ViewModels
{
    public class ReportBooksWithReadersViewModel
    {
        public DateTime DateOut { get; set; }
        public string Name { get; set; }
        public string Readers { get; set; }
    }
}
