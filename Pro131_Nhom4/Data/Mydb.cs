using App_Shared.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pro131_Nhom4.Data
{

    public class Mydb : IdentityDbContext<User,Role,Guid>
    {
        public Mydb(DbContextOptions<Mydb> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            CreateRoles(builder);
        }
        public DbSet<Carts> carts { get; set; }
        private void CreateRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                    new Role() { Name = "Admin", NormalizedName = "ADMIN",Id = Guid.NewGuid() },
                    new Role() { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid() }
                );
        }
    }

}
