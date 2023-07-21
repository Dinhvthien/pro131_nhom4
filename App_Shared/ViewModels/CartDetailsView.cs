using App_Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class CartDetailsView
    {
        public CartDetails CartDetails { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
