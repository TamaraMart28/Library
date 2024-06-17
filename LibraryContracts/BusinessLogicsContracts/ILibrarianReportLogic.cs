using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface ILibrarianReportLogic
    {
        List<ReportBooksWithReadersViewModel> GetBooksWithReaders(ReportBooksWithReadersBindingModel model);
        void SaveReadersBooksToPdf(ReportBooksWithReadersBindingModel model);
    }
}
