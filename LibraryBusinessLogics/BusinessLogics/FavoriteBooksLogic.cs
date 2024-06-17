using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;
using LibraryContracts.StoragesContracts;
using LibraryContracts.BusinessLogicsContracts;

namespace LibraryBusinessLogics.BusinessLogics
{
    public class FavoriteBooksLogic : IFavoriteBooksLogic
    {
        private readonly IFavoriteBooksStorage _fbStorage;

        public FavoriteBooksLogic(IFavoriteBooksStorage fbStorage)
        {
            _fbStorage = fbStorage;
        }

        public List<FavoriteBooksViewModel> Read(FavoriteBooksBindingModel model)
        {
            if (model == null)
            {
                return _fbStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<FavoriteBooksViewModel> { _fbStorage.GetElement(model) };
            }
            return _fbStorage.GetFilteredList(model);
        }

        public List<FavoriteBooksViewModel> ReadByIds(FavoriteBooksBindingModel model)
        {
            if (model == null)
            {
                return _fbStorage.GetFullList();
            }
            return new List<FavoriteBooksViewModel> { _fbStorage.GetElementByIds(model) };
        }

        public void CreateOrUpdate(FavoriteBooksBindingModel model)
        {
            var element = _fbStorage.GetElement(new FavoriteBooksBindingModel
            {
                ReaderId = model.ReaderId,
                BookId = model.BookId
            });
            if (element != null && element.Id != model.Id)
            {
                return;
            }
            else
            {
                _fbStorage.Insert(model);
            }
        }

        public void Delete(FavoriteBooksBindingModel model)
        {
            var element = _fbStorage.GetElement(new FavoriteBooksBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _fbStorage.Delete(model);
        }
    }
}
