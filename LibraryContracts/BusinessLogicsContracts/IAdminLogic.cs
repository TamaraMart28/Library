using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.ViewModels;
using LibraryContracts.BindingModels;

namespace LibraryContracts.BusinessLogicsContracts
{
    public interface IAdminLogic
    {
        List<AdminViewModel> Read(AdminBindingModel model);
        void Create(AdminBindingModel model);
    }
}
