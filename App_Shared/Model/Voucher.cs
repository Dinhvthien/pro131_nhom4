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
        [RegularExpression(@"\d*[aA-zZ]\d*", ErrorMessage = "The field name only has input number")]
        public string VoucherName { get; set; }
        [RegularExpression(@"\d*[0-9]\d*", ErrorMessage = "The field PercenDiscount only has input number")]
        public double PercenDiscount { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
