﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pro131_Nhom4.Data;

#nullable disable

namespace Pro131_Nhom4.Migrations
{
    [DbContext(typeof(Mydb))]
    [Migration("20230811050359_hoclaitiep")]
    partial class hoclaitiep
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("App_Shared.Model.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("BillStatusIdStt")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PayMentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("StatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VoucherID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountID");

                    b.HasIndex("BillStatusIdStt");

                    b.HasIndex("PayMentID");

                    b.HasIndex("VoucherID");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("App_Shared.Model.BillDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BillID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Prices")
                        .HasColumnType("float");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillID");

                    b.HasIndex("ProductID");

                    b.ToTable("Billdetails");
                });

            modelBuilder.Entity("App_Shared.Model.BillStatus", b =>
                {
                    b.Property<Guid>("IdStt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdStt");

                    b.ToTable("BillStatuses");

                    b.HasData(
                        new
                        {
                            IdStt = new Guid("968e5ad7-7c80-4ee7-8421-b5ba48e931ca"),
                            Name = "Chờ xác nhận"
                        },
                        new
                        {
                            IdStt = new Guid("62492de7-e53b-495f-8b36-238fef858392"),
                            Name = "Chờ lấy hàng"
                        },
                        new
                        {
                            IdStt = new Guid("d3f25cac-232f-4a48-baaa-17e897c8b2b8"),
                            Name = "Đang giao hàng"
                        },
                        new
                        {
                            IdStt = new Guid("d7f9bbae-36ca-4dc5-aff3-8a0e52567e25"),
                            Name = "Giao hàng thành công"
                        },
                        new
                        {
                            IdStt = new Guid("a62f61f4-4ef3-4afe-badd-55e0856f0f67"),
                            Name = "Giao hàng thất bại"
                        });
                });

            modelBuilder.Entity("App_Shared.Model.Cart", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("App_Shared.Model.CartDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CartUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartUserID");

                    b.HasIndex("ProductID");

                    b.ToTable("Cartdetails");
                });

            modelBuilder.Entity("App_Shared.Model.Colors", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("App_Shared.Model.FavoriteProducts", b =>
                {
                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("FavoriteProducts");
                });

            modelBuilder.Entity("App_Shared.Model.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("App_Shared.Model.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<Guid>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("SizeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorID");

                    b.HasIndex("SizeID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("App_Shared.Model.Rank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Point")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Ranks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("02f4cf23-3b1d-49dd-b89c-598185786e79"),
                            Name = "Sắt",
                            Point = 1.0
                        },
                        new
                        {
                            Id = new Guid("a1cc176f-780d-4c51-a6b1-581c622a7e0a"),
                            Name = "Đồng",
                            Point = 100.0
                        },
                        new
                        {
                            Id = new Guid("9dc43899-a1a8-4993-bdce-8348539796da"),
                            Name = "Bạc",
                            Point = 500.0
                        },
                        new
                        {
                            Id = new Guid("6c742186-4291-4950-b5e5-2ffb8b84ba09"),
                            Name = "Vàng",
                            Point = 1000.0
                        },
                        new
                        {
                            Id = new Guid("20758a88-a5f3-4b43-bd52-ec1f4484f78b"),
                            Name = "Kim Cương",
                            Point = 3000.0
                        },
                        new
                        {
                            Id = new Guid("2477b14e-9a1e-40f9-94d8-bba140a3c4e0"),
                            Name = "Thách đấu",
                            Point = 10000.0
                        });
                });

            modelBuilder.Entity("App_Shared.Model.Sizes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("App_Shared.Model.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("PercenDiscount")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("VoucherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Pro131_Nhom4.Data.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a464889-758d-49e8-b3df-5c7af78883b6"),
                            ConcurrencyStamp = "3c56699a-d955-486d-b3dc-7dbe1c62a3d7",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("b108d866-eb13-46e3-b3d2-ecae4fbfe873"),
                            ConcurrencyStamp = "8baeeb6f-4777-4d2a-926b-8a94960396cd",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Pro131_Nhom4.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<double>("Point")
                        .HasColumnType("float");

                    b.Property<Guid>("RankID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RankID");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("App_Shared.Model.Bill", b =>
                {
                    b.HasOne("Pro131_Nhom4.Data.User", "Account")
                        .WithMany("Bills")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Shared.Model.BillStatus", "BillStatus")
                        .WithMany("Bills")
                        .HasForeignKey("BillStatusIdStt");

                    b.HasOne("App_Shared.Model.Payment", "Payment")
                        .WithMany("Bills")
                        .HasForeignKey("PayMentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Shared.Model.Voucher", "Voucher")
                        .WithMany("Bills")
                        .HasForeignKey("VoucherID");

                    b.Navigation("Account");

                    b.Navigation("BillStatus");

                    b.Navigation("Payment");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("App_Shared.Model.BillDetails", b =>
                {
                    b.HasOne("App_Shared.Model.Bill", "Bill")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Shared.Model.Product", "Product")
                        .WithMany("BillDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("App_Shared.Model.Cart", b =>
                {
                    b.HasOne("Pro131_Nhom4.Data.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("App_Shared.Model.Cart", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App_Shared.Model.CartDetails", b =>
                {
                    b.HasOne("App_Shared.Model.Cart", "Cart")
                        .WithMany("CartDetails")
                        .HasForeignKey("CartUserID");

                    b.HasOne("App_Shared.Model.Product", "Product")
                        .WithMany("CartDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("App_Shared.Model.FavoriteProducts", b =>
                {
                    b.HasOne("Pro131_Nhom4.Data.User", "User")
                        .WithMany("FavoriteProducts")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Shared.Model.Product", "Products")
                        .WithMany("FavoriteProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Products");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App_Shared.Model.Product", b =>
                {
                    b.HasOne("App_Shared.Model.Colors", "Color")
                        .WithMany("Products")
                        .HasForeignKey("ColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Shared.Model.Sizes", "Size")
                        .WithMany("Products")
                        .HasForeignKey("SizeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Pro131_Nhom4.Data.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Pro131_Nhom4.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Pro131_Nhom4.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Pro131_Nhom4.Data.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pro131_Nhom4.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Pro131_Nhom4.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pro131_Nhom4.Data.User", b =>
                {
                    b.HasOne("App_Shared.Model.Rank", "Rank")
                        .WithMany("User")
                        .HasForeignKey("RankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("App_Shared.Model.Bill", b =>
                {
                    b.Navigation("BillDetails");
                });

            modelBuilder.Entity("App_Shared.Model.BillStatus", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("App_Shared.Model.Cart", b =>
                {
                    b.Navigation("CartDetails");
                });

            modelBuilder.Entity("App_Shared.Model.Colors", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("App_Shared.Model.Payment", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("App_Shared.Model.Product", b =>
                {
                    b.Navigation("BillDetails");

                    b.Navigation("CartDetails");

                    b.Navigation("FavoriteProducts");
                });

            modelBuilder.Entity("App_Shared.Model.Rank", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("App_Shared.Model.Sizes", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("App_Shared.Model.Voucher", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("Pro131_Nhom4.Data.User", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Cart");

                    b.Navigation("FavoriteProducts");
                });
#pragma warning restore 612, 618
        }
    }
}