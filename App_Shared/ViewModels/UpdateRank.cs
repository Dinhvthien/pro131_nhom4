using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_Shared.Model;

namespace App_Shared.ViewModels
{
    public class UpdateRank
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Point { get; set; }
    }
}
