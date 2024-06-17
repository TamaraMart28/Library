using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.StoragesContracts
{
    public interface IBookStorage
    {
        List<BookViewModel> GetFullList();
        List<BookViewModel> GetFilteredList(BookBindingModel model);
        BookViewModel GetElement(BookBindingModel model);
        void Insert(BookBindingModel model);
        void Update(BookBindingModel model);
        void Delete(BookBindingModel model);
    }
}
