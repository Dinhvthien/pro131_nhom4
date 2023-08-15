using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.Model
{
    public class Sizes
    {
        [Key]
        public Guid Id { get; set; }
        [RegularExpression(@"\d*[0-9]\d*", ErrorMessage = "The field Name only has input number")]
        public string Name { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
