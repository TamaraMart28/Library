using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.StoragesContracts
{
    public interface IReaderBookStorage
    {
        List<ReaderBookViewModel> GetFullList();
        List<ReaderBookViewModel> GetFilteredList(ReaderBookBindingModel model);
        List<ReaderBookViewModel> GetFilteredListByBookId(ReaderBookBindingModel model);//tome нужен этот метод
        ReaderBookViewModel GetElement(ReaderBookBindingModel model);
        void Insert(ReaderBookBindingModel model);
        void Update(ReaderBookBindingModel model);
        void Delete(ReaderBookBindingModel model);
    }
}
