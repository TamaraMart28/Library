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
    public class BranchLogic : IBranchLogic
    {
        private readonly IBranchStorage _branchStorage;

        public BranchLogic(IBranchStorage branchStorage)
        {
            _branchStorage = branchStorage;
        }

        public List<BranchViewModel> Read(BranchBindingModel model)
        {
            if (model == null)
            {
                return _branchStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<BranchViewModel> { _branchStorage.GetElement(model) };
            }
            return _branchStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(BranchBindingModel model)
        {
            var element = _branchStorage.GetElement(new BranchBindingModel
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
            }
        }

        public void Delete(BranchBindingModel model)
        {
            var element = _branchStorage.GetElement(new BranchBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _branchStorage.Delete(model);
        }
    }
}
