using App_Shared.Model;
using Pro131_Nhom4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class BillView
    {

        public Bill Bill { get; set; }
        public Voucher Voucher { get; set; }
        public User User { get; set; }
        public BillStatus BillStatus { get; set; }
        public Payment Payment { get; set; }
    }
}
