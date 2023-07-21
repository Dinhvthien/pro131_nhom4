using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.Model
{
    public class Voucher
    {
        [Key]
        public Guid Id { get; set; }
        public string VoucherName { get; set; }
        public double PercenDiscount { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
