using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Pro131_Nhom4.Data;

namespace App_Shared.Model
{
    public class Bill
    {
        [Key]
        public Guid Id { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("PayMentID")]
        public Guid PayMentID { get; set; }
        [ForeignKey("StatusID")]
        public Guid StatusID { get; set; }
        [ForeignKey("VoucherID")]
        public Guid? VoucherID { get; set; }
        public string Address { get; set; }
        [ForeignKey("AccountID")]
        public Guid AccountID { get; set; }
        public virtual ICollection<BillDetails>? BillDetails { get; set; }
        public virtual Voucher? Voucher { get; set; }
        public virtual User? Account { get; set; }
        public virtual BillStatus? BillStatus { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}
