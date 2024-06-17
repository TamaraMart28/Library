using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface ILibrarianLogic
    {
        List<LibrarianViewModel> Read(LibrarianBindingModel model);
        void CreateOrUpdate(LibrarianBindingModel model);
        void Delete(LibrarianBindingModel model);
    }
}
