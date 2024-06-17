using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryContracts.StoragesContracts
{
    public interface IAdminStorage
    {
        List<AdminViewModel> GetFullList();
        List<AdminViewModel> GetFilteredList(AdminBindingModel model);
        void Insert(AdminBindingModel model);
        void Update(AdminBindingModel model);
        void Delete(AdminBindingModel model);
    }
}
