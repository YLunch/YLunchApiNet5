﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YnovEat.Infrastructure.Database;

namespace YnovEat.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210610130136_V1.1")]
    partial class V11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RestaurantRestaurantCategory", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "RestaurantsId");

                    b.HasIndex("RestaurantsId");

                    b.ToTable("RestaurantRestaurantCategory");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Cart", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("FulfilmentDatetime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CustomerId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Customer", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("CustomerFamily")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.CustomerAggregate.CustomerProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CartCustomerId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("RestaurantProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartCustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("CustomerProduct");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.ClosingDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ClosingDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("ClosingDates");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.DayOpeningHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("DayOpeningHours");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.OpeningHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("DayOpeningHoursId")
                        .HasColumnType("int");

                    b.Property<int>("EndHourInMinutes")
                        .HasColumnType("int");

                    b.Property<int>("StartHourInMinutes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayOpeningHoursId");

                    b.ToTable("OpeningHours");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("AcceptationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EmailConfirmationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ExtraInformation")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastUpdateDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("OrderLimitTimeInMinutes")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("StreetName")
                        .HasColumnType("longtext");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("ZipCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RestaurantCategories");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ExpirationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("ProductFamily")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("RestaurantProducts");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProductTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RestaurantProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantProductId");

                    b.ToTable("RestaurantProductTags");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("RestaurantUser");

                    b.HasDiscriminator<string>("Discriminator").HasValue("RestaurantUser");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.UserAggregate.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ConfirmationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("EmailConfirmationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAccountActivated")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastUpdateDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("PhoneNumberConfirmationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantOwner", b =>
                {
                    b.HasBaseType("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser");

                    b.HasDiscriminator().HasValue("RestaurantOwner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YnovEat.Domain.ModelsAggregate.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantRestaurantCategory", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", null)
                        .WithMany()
                        .HasForeignKey("RestaurantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Cart", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Customer", "Customer")
                        .WithOne("Cart")
                        .HasForeignKey("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Cart", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Customer", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.UserAggregate.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.CustomerAggregate.CustomerProduct", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Cart", "Cart")
                        .WithMany("Products")
                        .HasForeignKey("CartCustomerId");

                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Order", "Order")
                        .WithMany("CustomerProducts")
                        .HasForeignKey("OrderId");

                    b.Navigation("Cart");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.ClosingDate", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "Restaurant")
                        .WithMany("ClosingDates")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.DayOpeningHours", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "Restaurant")
                        .WithMany("DaysOpeningHours")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.OpeningHour", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.DayOpeningHours", "DayOpeningHours")
                        .WithMany("OpeningHours")
                        .HasForeignKey("DayOpeningHoursId");

                    b.Navigation("DayOpeningHours");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Order", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.OrderStatus", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Order", "Order")
                        .WithMany("OrderStatuses")
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantOwner", "Owner")
                        .WithOne("Restaurant")
                        .HasForeignKey("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProduct", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Order", null)
                        .WithMany("RestaurantProducts")
                        .HasForeignKey("OrderId");

                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "Restaurant")
                        .WithMany("Products")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProductTag", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProduct", null)
                        .WithMany("RestaurantProductTags")
                        .HasForeignKey("RestaurantProductId");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", null)
                        .WithMany("RestaurantUsers")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.UserAggregate.User", b =>
                {
                    b.HasOne("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser", "RestaurantUser")
                        .WithOne("User")
                        .HasForeignKey("YnovEat.Domain.ModelsAggregate.UserAggregate.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RestaurantUser");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Cart", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.CustomerAggregate.Customer", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.DayOpeningHours", b =>
                {
                    b.Navigation("OpeningHours");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Order", b =>
                {
                    b.Navigation("CustomerProducts");

                    b.Navigation("OrderStatuses");

                    b.Navigation("RestaurantProducts");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", b =>
                {
                    b.Navigation("ClosingDates");

                    b.Navigation("DaysOpeningHours");

                    b.Navigation("Products");

                    b.Navigation("RestaurantUsers");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProduct", b =>
                {
                    b.Navigation("RestaurantProductTags");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.UserAggregate.User", b =>
                {
                    b.Navigation("Customer");
                });

            modelBuilder.Entity("YnovEat.Domain.ModelsAggregate.RestaurantAggregate.RestaurantOwner", b =>
                {
                    b.Navigation("Restaurant");
                });
#pragma warning restore 612, 618
        }
    }
}
