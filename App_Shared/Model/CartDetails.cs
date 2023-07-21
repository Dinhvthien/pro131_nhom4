using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.Model
{
    public class CartDetails
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("AccountID")]
        public Guid AccountID { get; set; }
        [ForeignKey("ProductID")]
        public Guid ProductID { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
