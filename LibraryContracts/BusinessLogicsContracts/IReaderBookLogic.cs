using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface IReaderBookLogic
    {
        List<ReaderBookViewModel> Read(ReaderBookBindingModel model);
        List<ReaderBookViewModel> ReadByBookId(ReaderBookBindingModel model); 
        void CreateOrUpdate(ReaderBookBindingModel model);
        void Delete(ReaderBookBindingModel model);
    }
}
