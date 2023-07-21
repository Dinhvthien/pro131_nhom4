using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class UpdateUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        [MaxLength(10)]
        [RegularExpression(@"\d*[0-9]\d*", ErrorMessage = "The field PhoneNumber only has input number")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
