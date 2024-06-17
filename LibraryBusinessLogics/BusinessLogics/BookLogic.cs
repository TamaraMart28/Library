using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.BusinessLogicsContracts;
using LibraryContracts.StoragesContracts;
using LibraryContracts.ViewModels;

namespace LibraryBusinessLogics.BusinessLogics
{
    public class BookLogic : IBookLogic
    {
        private readonly IBookStorage _bookStorage;

        public BookLogic(IBookStorage bookStorage)
        {
            _bookStorage = bookStorage;
        }

        public void CreateOrUpdate(BookBindingModel model)
        {
            var element = _bookStorage.GetElement(new BookBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть книга с таким названием");
            }
            if (model.Id.HasValue)
            {
                _bookStorage.Update(model);
            }
            else
            {
                _bookStorage.Insert(model);
            }
        }

        public void Delete(BookBindingModel model)
        {
            var element = _bookStorage.GetElement(new BookBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Книга не найдена");
            }
            _bookStorage.Delete(model);
        }

        public List<BookViewModel> Read(BookBindingModel model)
        {
            if (model == null)
            {
                return _bookStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<BookViewModel> { _bookStorage.GetElement(model) };
            }
            return _bookStorage.GetFilteredList(model);
        }
    }
}
