using App_Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class CreateSize
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }       
    }
}
