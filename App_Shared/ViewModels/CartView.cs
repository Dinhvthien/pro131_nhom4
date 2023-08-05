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
    public class CartView
    {
		public Cart Cart { get; set; }
		public User User { get; set; }
	}
}
