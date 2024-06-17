using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BusinessLogicsContracts;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;
using LibraryContracts.StoragesContracts;

namespace LibraryBusinessLogics.BusinessLogics
{
    public class BookBranchLogic : IBookBranchLogic
    {
        private readonly IBookBranchStorage _bbStorage;

        public BookBranchLogic(IBookBranchStorage bbStorage)
        {
            _bbStorage = bbStorage;
        }

        public List<BookBranchViewModel> Read(BookBranchBindingModel model)
        {
            if (model == null)
            {
                return _bbStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<BookBranchViewModel> { _bbStorage.GetElement(model) };
            }
            return _bbStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(BookBranchBindingModel model) //to do
        {
            /*var element = _bbStorage.GetElement(new BookBranchBindingModel
            {
                Address = model.Address
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть филиал по такому адресу");
            }
            if (model.Id.HasValue)
            {
                _branchStorage.Update(model);
            }
            else
            {
                _branchStorage.Insert(model);
            }*/
        }

        public void Delete(BookBranchBindingModel model)
        {
            var element = _bbStorage.GetElement(new BookBranchBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _bbStorage.Delete(model);
        }
    }
}
