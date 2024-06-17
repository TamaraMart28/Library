using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface IReaderReportLogic
    {
        List<ReportBooksViewModel> GetReadersBooks(ReportBooksBindingModel model);
        void SaveReadersBooksToPdf(ReportBooksBindingModel model);
    }
}
