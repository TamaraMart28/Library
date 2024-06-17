using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface IFavoriteBooksLogic
    {
        List<FavoriteBooksViewModel> Read(FavoriteBooksBindingModel model);
        List<FavoriteBooksViewModel> ReadByIds(FavoriteBooksBindingModel model);
        void CreateOrUpdate(FavoriteBooksBindingModel model);
        void Delete(FavoriteBooksBindingModel model);
    }
}
