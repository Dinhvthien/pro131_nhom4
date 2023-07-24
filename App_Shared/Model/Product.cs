using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.Model
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string ImageUrl { get; set; }
        public string Manufacturer { get; set; }
        public int Status { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
        [ForeignKey("ColorID")]
        public Guid ColorID { get; set; }
        [ForeignKey("SizeID")]
        public Guid SizeID { get; set; }
        public virtual ICollection<BillDetails>? BillDetails { get; set; }
        public virtual ICollection<CartDetails>? CartDetails { get; set; }
        public virtual ICollection<FavoriteProducts>? FavoriteProducts { get; set; }
        public virtual Colors? Color { get; set; }
        public virtual Sizes? Size { get; set; }
    }
}
