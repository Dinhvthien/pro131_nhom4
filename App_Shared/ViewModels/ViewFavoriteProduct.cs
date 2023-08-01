using App_Shared.Model;
using Pro131_Nhom4.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class ViewFavoriteProduct
    {
        public Guid? AccountID { get; set; }
        public Guid? ProductID { get; set; }
        public string? Description { get; set; }

        public string? NameProduct { get; set; }
        public double? PriceProduct { get; set; }
        public int? AvailableQuantityProduct { get; set; }
        public string? ImageUrlProduct { get; set; }
        public string? ManufacturerProduct { get; set; }
        public int? StatusProduct { get; set; }
        public int? LikesProduct { get; set; }
        public string? DescriptionProduct { get; set; }



    }
}
