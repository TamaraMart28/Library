using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.StoragesContracts
{
    public interface ILibrarianStorage
    {
        List<LibrarianViewModel> GetFullList();
        List<LibrarianViewModel> GetFilteredList(LibrarianBindingModel model);
        LibrarianViewModel GetElement(LibrarianBindingModel model);
        void Insert(LibrarianBindingModel model);
        void Update(LibrarianBindingModel model);
        void Delete(LibrarianBindingModel model);
    }
}
