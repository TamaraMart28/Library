using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface IBranchLogic
    {
        List<BranchViewModel> Read(BranchBindingModel model);
        void CreateOrUpdate(BranchBindingModel model);
        void Delete(BranchBindingModel model);
    }
}
