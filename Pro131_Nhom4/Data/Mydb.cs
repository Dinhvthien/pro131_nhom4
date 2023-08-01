using App_Shared.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Drawing;
using System.Reflection.Emit;
using System.Security.Principal;


namespace Pro131_Nhom4.Data
{

    public class Mydb : IdentityDbContext<User,Role,Guid>
    {
        public Mydb(DbContextOptions<Mydb> options) : base(options)
        {

        }

        public Mydb()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-VVKT5NE\\SQLEXPRESS;Initial Catalog=1311;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetails> Billdetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> Cartdetails { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<FavoriteProducts> FavoriteProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<BillStatus> BillStatuses { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FavoriteProducts>()
                .HasKey(p => new { p.AccountID, p.ProductID });
            builder.Entity<User>()
            .HasOne(a => a.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserID); // Specify the foreign key property
            base.OnModelCreating(builder);
            CreateRoles(builder);
        }
        private void CreateRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                    new Role() { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid() },
                    new Role() { Name = "User", NormalizedName = "USER", Id = Guid.Parse("b108d866-eb13-46e3-b3d2-ecae4fbfe873") }
                );
            builder.Entity<Rank>().HasData(
                    new Rank() { Name = "Sắt", Point = 1 , Id = Guid.Parse("02f4cf23-3b1d-49dd-b89c-598185786e79") },
                    new Rank() { Name = "Đồng", Point = 100, Id = Guid.NewGuid() },
                    new Rank() { Name = "Bạc", Point = 500, Id = Guid.NewGuid() },
                    new Rank() { Name = "Vàng", Point = 1000, Id = Guid.NewGuid() },
                    new Rank() { Name = "Kim Cương", Point = 3000, Id = Guid.NewGuid() },
                    new Rank() { Name = "Thách đấu", Point = 10000, Id = Guid.NewGuid() }
                    );
        }
   
    }

}
