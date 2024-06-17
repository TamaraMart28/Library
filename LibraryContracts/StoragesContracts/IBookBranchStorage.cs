using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.StoragesContracts
{
    public interface IBookBranchStorage
    {
        List<BookBranchViewModel> GetFullList();
        List<BookBranchViewModel> GetFilteredList(BookBranchBindingModel model);
        BookBranchViewModel GetElement(BookBranchBindingModel model);
        void Insert(BookBranchBindingModel model);
        void Update(BookBranchBindingModel model);
        void Delete(BookBranchBindingModel model);
    }
}
