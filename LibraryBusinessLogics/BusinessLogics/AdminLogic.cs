using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.StoragesContracts;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;
using LibraryContracts.BusinessLogicsContracts;

namespace LibraryBusinessLogics.BusinessLogics
{
    public class AdminLogic : IAdminLogic
    {
        private readonly IAdminStorage _adminStorage;

        public AdminLogic(IAdminStorage adminStorage)
        {
            _adminStorage = adminStorage;
        }

        public List<AdminViewModel> Read(AdminBindingModel model)
        {
            return _adminStorage.GetFilteredList(model);
        }

        public void Create(AdminBindingModel model)
        {
            _adminStorage.Insert(model);
        }
    }
}
