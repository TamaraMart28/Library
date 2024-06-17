using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.ViewModels;
using LibraryContracts.BindingModels;

namespace LibraryContracts.StoragesContracts
{
    public interface IBranchStorage
    {
        List<BranchViewModel> GetFullList();
        List<BranchViewModel> GetFilteredList(BranchBindingModel model);
        BranchViewModel GetElement(BranchBindingModel model);
        void Insert(BranchBindingModel model);
        void Update(BranchBindingModel model);
        void Delete(BranchBindingModel model);

    }
}
