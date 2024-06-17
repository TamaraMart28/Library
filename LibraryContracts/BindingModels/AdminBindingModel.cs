using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.BindingModels
{
    public class AdminBindingModel
    {
        public int? Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
