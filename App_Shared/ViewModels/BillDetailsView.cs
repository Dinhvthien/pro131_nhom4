using App_Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class BillDetailsView
    {
        public BillDetails BillDetails { get; set; }
        public Bill Bill { get; set; }
        public Product Products { get; set; }
    }
}
