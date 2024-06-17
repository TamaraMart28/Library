using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.StoragesContracts
{
    public interface IReaderStorage
    {
        List<ReaderViewModel> GetFullList();
        List<ReaderViewModel> GetFilteredList(ReaderBindingModel model);
        ReaderViewModel GetElement(ReaderBindingModel model);
        void Insert(ReaderBindingModel model);
        void Update(ReaderBindingModel model);
        void Delete(ReaderBindingModel model);
    }
}
