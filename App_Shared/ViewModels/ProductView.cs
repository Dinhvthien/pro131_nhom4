using System;
using System.Collections.Generic;
using App_Shared.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class ProductView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string ImageUrl { get; set; }
        public string Manufacturer { get; set; }
        public int Status { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
        public Guid ColorID { get; set; }
        public Guid SizeID { get; set; }
        public Colors? Color { get; set; }
        public Sizes? Size { get; set; }
    }
}
