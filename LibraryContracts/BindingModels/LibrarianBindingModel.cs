﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryContracts.BindingModels
{
    public class LibrarianBindingModel
    {
        public int? Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public int BranchId { get; set; }
    }
}
