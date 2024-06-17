using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;


namespace LibraryContracts.StoragesContracts
{
    public interface IFavoriteBooksStorage
    {
        List<FavoriteBooksViewModel> GetFullList();
        List<FavoriteBooksViewModel> GetFilteredList(FavoriteBooksBindingModel model);
        FavoriteBooksViewModel GetElement(FavoriteBooksBindingModel model);
        FavoriteBooksViewModel GetElementByIds(FavoriteBooksBindingModel model);
        void Insert(FavoriteBooksBindingModel model);
        void Delete(FavoriteBooksBindingModel model);
    }
}
