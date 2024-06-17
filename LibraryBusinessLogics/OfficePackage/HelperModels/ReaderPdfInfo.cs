using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.ViewModels;

namespace LibraryBusinessLogics.OfficePackage.HelperModels
{
    public class ReaderPdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<ReportBooksViewModel> ReadersBooks { get; set; }
    }
}
