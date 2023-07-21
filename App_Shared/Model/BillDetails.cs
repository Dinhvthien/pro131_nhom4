using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.Model
{
    public class BillDetails
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Prices { get; set; }
        [ForeignKey("BillID")]
        public Guid BillID { get; set; }
        [ForeignKey("ProductID")]
        public Guid ProductID { get; set; }
        public virtual Bill? Bill { get; set; }
        public virtual Product? Product { get; set; }
    }
}
