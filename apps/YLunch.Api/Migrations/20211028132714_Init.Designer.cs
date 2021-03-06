// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YLunch.Infrastructure.Database;

namespace YLunch.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211028132714_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RestaurantRestaurantCategory", b =>
                {
                    b.Property<string>("CategoriesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RestaurantsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CategoriesId", "RestaurantsId");

                    b.HasIndex("RestaurantsId");

                    b.ToTable("RestaurantRestaurantCategory");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.CustomerAggregate.Cart", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("FulfilmentDatetime")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.CustomerAggregate.Customer", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CustomerFamily")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.CustomerAggregate.CustomerProduct", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CartUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("RestaurantId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestaurantProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CartUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("CustomerProducts");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.ClosingDate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ClosingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RestaurantId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("ClosingDates");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.DayOpeningTimes", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<string>("RestaurantId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("DaysOpeningTimes");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.OpeningTime", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DayOpeningTimesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("EndOrderTimeInMinutes")
                        .HasColumnType("int");

                    b.Property<int>("EndTimeInMinutes")
                        .HasColumnType("int");

                    b.Property<int?>("StartOrderTimeInMinutes")
                        .HasColumnType("int");

                    b.Property<int>("StartTimeInMinutes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayOpeningTimesId");

                    b.ToTable("OpeningTimes");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("AcceptationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ReservedForDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RestaurantComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestaurantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.OrderStatus", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressExtraInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Base64Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Base64Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmailConfirmationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RestaurantCategories");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProduct", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductFamily")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("RestaurantId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("RestaurantProducts");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProductTag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestaurantProductId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantProductId");

                    b.ToTable("RestaurantProductTags");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestaurantId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("RestaurantUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("RestaurantUser");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.UserAggregate.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ConfirmationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("EmailConfirmationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAccountActivated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PhoneNumberConfirmationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantAdmin", b =>
                {
                    b.HasBaseType("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser");

                    b.HasDiscriminator().HasValue("RestaurantAdmin");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantEmployee", b =>
                {
                    b.HasBaseType("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser");

                    b.HasDiscriminator().HasValue("RestaurantEmployee");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantOwner", b =>
                {
                    b.HasBaseType("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser");

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
                    b.HasOne("YLunch.Domain.ModelsAggregate.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.UserAggregate.User", null)
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

                    b.HasOne("YLunch.Domain.ModelsAggregate.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantRestaurantCategory", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", null)
                        .WithMany()
                        .HasForeignKey("RestaurantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.CustomerAggregate.Cart", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.CustomerAggregate.Customer", "Customer")
                        .WithOne("Cart")
                        .HasForeignKey("YLunch.Domain.ModelsAggregate.CustomerAggregate.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.CustomerAggregate.Customer", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.UserAggregate.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("YLunch.Domain.ModelsAggregate.CustomerAggregate.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.CustomerAggregate.CustomerProduct", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.CustomerAggregate.Cart", null)
                        .WithMany("Products")
                        .HasForeignKey("CartUserId");

                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Order", "Order")
                        .WithMany("CustomerProducts")
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.ClosingDate", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "Restaurant")
                        .WithMany("ClosingDates")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.DayOpeningTimes", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "Restaurant")
                        .WithMany("WeekOpeningTimes")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.OpeningTime", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.DayOpeningTimes", "DayOpeningTimes")
                        .WithMany("OpeningTimes")
                        .HasForeignKey("DayOpeningTimesId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("DayOpeningTimes");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Order", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.CustomerAggregate.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Customer");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.OrderStatus", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Order", "Order")
                        .WithMany("OrderStatuses")
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantOwner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerUserId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProduct", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "Restaurant")
                        .WithMany("RestaurantProducts")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProductTag", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProduct", null)
                        .WithMany("RestaurantProductTags")
                        .HasForeignKey("RestaurantProductId");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser", b =>
                {
                    b.HasOne("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", "Restaurant")
                        .WithMany("RestaurantUsers")
                        .HasForeignKey("RestaurantId");

                    b.HasOne("YLunch.Domain.ModelsAggregate.UserAggregate.User", "User")
                        .WithOne("RestaurantUser")
                        .HasForeignKey("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.CustomerAggregate.Cart", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.CustomerAggregate.Customer", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.DayOpeningTimes", b =>
                {
                    b.Navigation("OpeningTimes");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Order", b =>
                {
                    b.Navigation("CustomerProducts");

                    b.Navigation("OrderStatuses");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.Restaurant", b =>
                {
                    b.Navigation("ClosingDates");

                    b.Navigation("Orders");

                    b.Navigation("RestaurantProducts");

                    b.Navigation("RestaurantUsers");

                    b.Navigation("WeekOpeningTimes");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.RestaurantAggregate.RestaurantProduct", b =>
                {
                    b.Navigation("RestaurantProductTags");
                });

            modelBuilder.Entity("YLunch.Domain.ModelsAggregate.UserAggregate.User", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("RestaurantUser");
                });
#pragma warning restore 612, 618
        }
    }
}
