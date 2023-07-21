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
    public class FavoriteProductsView
    {
        public FavoriteProducts FavoriteProducts { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
