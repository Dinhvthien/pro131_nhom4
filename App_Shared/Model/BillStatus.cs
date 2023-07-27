﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.Model
{
    public class BillStatus
    {
        [Key]
        public Guid IdStt { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}