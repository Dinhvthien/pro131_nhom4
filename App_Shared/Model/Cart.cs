using Pro131_Nhom4.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.Model
{
    public class Cart
    {
        [Key]
        public Guid UserID { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<CartDetails>? CartDetails { get; set; }
        public virtual User? User { get; set; }
    }
}
