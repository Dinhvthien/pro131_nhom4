using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Shared.ViewModels
{
    public class RegisterUser
    {
        public Guid id { get; set; }
        [Required(ErrorMessage = "Username cannot be blank")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength(10)]
        [RegularExpression(@"\d*[0-9]\d*", ErrorMessage = "The field PhoneNumber only has input number")]
        public string PhoneNumber { get; set; }
        [Required]
        public int? Gender { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Password cannot be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password cannot be blank")]
        public string? ConfirmPassword { get; set; }
    }
}
