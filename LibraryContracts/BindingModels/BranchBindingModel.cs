using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.BindingModels
{
    public class BranchBindingModel
    {
        public int? Id { get; set; }

        public string Address { get; set; }

        public string Schedule { get; set; }

        public double Сoordinate1 { get; set; }

        public double Сoordinate2 { get; set; }

        public Dictionary<int, string> Librarians { get; set; }

        
        public Dictionary<int, string> BookBranches { get; set; }
    }
}
