using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface IBookBranchLogic
    {
        List<BookBranchViewModel> Read(BookBranchBindingModel model);
        void CreateOrUpdate(BookBranchBindingModel model);
        void Delete(BookBranchBindingModel model);
    }
}
