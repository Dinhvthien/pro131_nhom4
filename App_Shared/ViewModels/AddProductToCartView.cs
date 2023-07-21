using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class AddProductToCartView
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Guid ColorID { get; set; }
        public Guid SizeID { get; set; }
    }
}
