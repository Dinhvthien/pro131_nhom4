using Microsoft.AspNetCore.Identity;
using Pro131_Nhom4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.Model
{
    public class Carts
    {
        public Guid id { get; set; }
        public ICollection<User> user { get; set; }
    }
}
