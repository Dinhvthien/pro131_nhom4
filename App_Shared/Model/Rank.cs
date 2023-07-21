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
    public class Rank
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Point { get; set; }
        public virtual ICollection<User>? User { get; set; }
    }
}
