using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface IReaderLogic
    {
        List<ReaderViewModel> Read(ReaderBindingModel model);
        void CreateOrUpdate(ReaderBindingModel model);
        void Delete(ReaderBindingModel model);
    }
}
