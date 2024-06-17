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
    public class ReaderBookLogic : IReaderBookLogic
    {
        private readonly IReaderBookStorage _rbStorage;

        public ReaderBookLogic(IReaderBookStorage rbStorage)
        {
            _rbStorage = rbStorage;
        }

        public List<ReaderBookViewModel> Read(ReaderBookBindingModel model)
        {
            if (model == null)
            {
                return _rbStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ReaderBookViewModel> { _rbStorage.GetElement(model) };
            }
            return _rbStorage.GetFilteredList(model);
        }

        public List<ReaderBookViewModel> ReadByBookId(ReaderBookBindingModel model)
        {
            return _rbStorage.GetFilteredListByBookId(model);
        }

        public void CreateOrUpdate(ReaderBookBindingModel model) //to do
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

        public void Delete(ReaderBookBindingModel model)// to do
        {
            //var element = _bbStorage.GetElement(new BookBranchBindingModel { Id = model.Id });
            //if (element == null)
            //{
            //    throw new Exception("Элемент не найден");
            //}
            //_bbStorage.Delete(model);
        }
    }
}
