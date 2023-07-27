using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class CreateFavoriteProducts
    {
        public Guid AccountID { get; set; }      
        public Guid ProductID { get; set; }
        public string Description { get; set; }
    }
}
