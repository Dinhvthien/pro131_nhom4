    using App_Shared.Model;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pro131_Nhom4.Data
{
    public class User: IdentityUser<Guid>
    {
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        [MaxLength(10)]
        [RegularExpression(@"\d*[0-9]\d*", ErrorMessage = "The field PhoneNumber only has input number")]
        public string PhoneNumber { get; set; }
        public double Point { get; set; }
        public string  Address { get; set; }
        public int Status { get; set; }
        [ForeignKey("RoleId")]
        public Guid RoleId { get; set; }
        [ForeignKey("RankID")]
        public Guid RankID { get; set; }

        public virtual ICollection<Bill>? Bills { get; set; }
        public virtual ICollection<FavoriteProducts>? FavoriteProducts { get; set; }
        public virtual Rank? Rank { get; set; }
        public virtual Cart? Cart { get; set; }
    }

}
