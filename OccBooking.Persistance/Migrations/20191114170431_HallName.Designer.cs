﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Persistance.Migrations
{
    [DbContext(typeof(OccBookingDbContext))]
    [Migration("20191114170431_HallName")]
    partial class HallName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Hall", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<string>("Name");

                    b.Property<Guid?>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.HallJoin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("FirstHallId");

                    b.Property<Guid?>("SecondHallId");

                    b.HasKey("Id");

                    b.HasIndex("FirstHallId");

                    b.HasIndex("SecondHallId");

                    b.ToTable("HallJoins");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.HallReservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("HallId");

                    b.Property<Guid?>("ReservationRequestId");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("ReservationRequestId");

                    b.ToTable("HallReservations");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Ingredients");

                    b.Property<Guid?>("MenuId");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CostPerPerson");

                    b.Property<string>("Name");

                    b.Property<Guid?>("PlaceId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Place", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalOptions");

                    b.Property<string>("AvailableOccasionTypes");

                    b.Property<decimal>("CostPerPerson");

                    b.Property<string>("Description");

                    b.Property<bool>("HasRooms");

                    b.Property<string>("Name");

                    b.Property<Guid?>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.ReservationRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalOptions");

                    b.Property<int>("AmountOfPeople");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("DateTime");

                    b.Property<bool>("IsAccepted");

                    b.Property<bool>("IsRejected");

                    b.Property<Guid?>("MenuId");

                    b.Property<Guid?>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("PlaceId");

                    b.ToTable("ReservationRequests");
                });

            modelBuilder.Entity("OccBooking.Persistance.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fasola"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ziemniaki"
                        });
                });

            modelBuilder.Entity("OccBooking.Persistance.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<Guid?>("OwnerId");

                    b.HasIndex("OwnerId");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Hall", b =>
                {
                    b.HasOne("OccBooking.Domain.Entities.Place", "Place")
                        .WithMany("Halls")
                        .HasForeignKey("PlaceId");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.HallJoin", b =>
                {
                    b.HasOne("OccBooking.Domain.Entities.Hall", "FirstHall")
                        .WithMany("PossibleJoinsWhereIsFirst")
                        .HasForeignKey("FirstHallId");

                    b.HasOne("OccBooking.Domain.Entities.Hall", "SecondHall")
                        .WithMany("PossibleJoinsWhereIsSecond")
                        .HasForeignKey("SecondHallId");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.HallReservation", b =>
                {
                    b.HasOne("OccBooking.Domain.Entities.Hall", "Hall")
                        .WithMany("HallReservations")
                        .HasForeignKey("HallId");

                    b.HasOne("OccBooking.Domain.Entities.ReservationRequest", "ReservationRequest")
                        .WithMany()
                        .HasForeignKey("ReservationRequestId");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Meal", b =>
                {
                    b.HasOne("OccBooking.Domain.Entities.Menu", "Menu")
                        .WithMany("Meals")
                        .HasForeignKey("MenuId");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Menu", b =>
                {
                    b.HasOne("OccBooking.Domain.Entities.Place", "Place")
                        .WithMany("Menus")
                        .HasForeignKey("PlaceId");
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Owner", b =>
                {
                    b.OwnsOne("OccBooking.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("OwnerId");

                            b1.Property<string>("Value");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Owners");

                            b1.HasOne("OccBooking.Domain.Entities.Owner")
                                .WithOne("Email")
                                .HasForeignKey("OccBooking.Domain.ValueObjects.Email", "OwnerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("OccBooking.Domain.ValueObjects.PersonName", "Name", b1 =>
                        {
                            b1.Property<Guid>("OwnerId");

                            b1.Property<string>("FirstName");

                            b1.Property<string>("LastName");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Owners");

                            b1.HasOne("OccBooking.Domain.Entities.Owner")
                                .WithOne("Name")
                                .HasForeignKey("OccBooking.Domain.ValueObjects.PersonName", "OwnerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("OccBooking.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("OwnerId");

                            b1.Property<string>("Value");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Owners");

                            b1.HasOne("OccBooking.Domain.Entities.Owner")
                                .WithOne("PhoneNumber")
                                .HasForeignKey("OccBooking.Domain.ValueObjects.PhoneNumber", "OwnerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.Place", b =>
                {
                    b.HasOne("OccBooking.Domain.Entities.Owner", "Owner")
                        .WithMany("Places")
                        .HasForeignKey("OwnerId");

                    b.OwnsOne("OccBooking.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("PlaceId");

                            b1.Property<string>("City");

                            b1.Property<string>("Province");

                            b1.Property<string>("Street");

                            b1.Property<string>("ZipCode");

                            b1.HasKey("PlaceId");

                            b1.ToTable("Places");

                            b1.HasOne("OccBooking.Domain.Entities.Place")
                                .WithOne("Address")
                                .HasForeignKey("OccBooking.Domain.ValueObjects.Address", "PlaceId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("OccBooking.Domain.Entities.ReservationRequest", b =>
                {
                    b.HasOne("OccBooking.Domain.Entities.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId");

                    b.HasOne("OccBooking.Domain.Entities.Place", "Place")
                        .WithMany("ReservationRequests")
                        .HasForeignKey("PlaceId");

                    b.OwnsOne("OccBooking.Domain.ValueObjects.Client", "Client", b1 =>
                        {
                            b1.Property<Guid>("ReservationRequestId");

                            b1.HasKey("ReservationRequestId");

                            b1.ToTable("ReservationRequests");

                            b1.HasOne("OccBooking.Domain.Entities.ReservationRequest")
                                .WithOne("Client")
                                .HasForeignKey("OccBooking.Domain.ValueObjects.Client", "ReservationRequestId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("OccBooking.Domain.ValueObjects.Email", "Email", b2 =>
                                {
                                    b2.Property<Guid>("ClientReservationRequestId");

                                    b2.Property<string>("Value");

                                    b2.HasKey("ClientReservationRequestId");

                                    b2.ToTable("ReservationRequests");

                                    b2.HasOne("OccBooking.Domain.ValueObjects.Client")
                                        .WithOne("Email")
                                        .HasForeignKey("OccBooking.Domain.ValueObjects.Email", "ClientReservationRequestId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("OccBooking.Domain.ValueObjects.PersonName", "Name", b2 =>
                                {
                                    b2.Property<Guid>("ClientReservationRequestId");

                                    b2.Property<string>("FirstName");

                                    b2.Property<string>("LastName");

                                    b2.HasKey("ClientReservationRequestId");

                                    b2.ToTable("ReservationRequests");

                                    b2.HasOne("OccBooking.Domain.ValueObjects.Client")
                                        .WithOne("Name")
                                        .HasForeignKey("OccBooking.Domain.ValueObjects.PersonName", "ClientReservationRequestId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("OccBooking.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b2 =>
                                {
                                    b2.Property<Guid>("ClientReservationRequestId");

                                    b2.Property<string>("Value");

                                    b2.HasKey("ClientReservationRequestId");

                                    b2.ToTable("ReservationRequests");

                                    b2.HasOne("OccBooking.Domain.ValueObjects.Client")
                                        .WithOne("PhoneNumber")
                                        .HasForeignKey("OccBooking.Domain.ValueObjects.PhoneNumber", "ClientReservationRequestId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("OccBooking.Domain.ValueObjects.OccasionType", "OccasionType", b1 =>
                        {
                            b1.Property<Guid>("ReservationRequestId");

                            b1.Property<string>("Name");

                            b1.HasKey("ReservationRequestId");

                            b1.ToTable("ReservationRequests");

                            b1.HasOne("OccBooking.Domain.Entities.ReservationRequest")
                                .WithOne("OccasionType")
                                .HasForeignKey("OccBooking.Domain.ValueObjects.OccasionType", "ReservationRequestId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("OccBooking.Persistance.Entities.User", b =>
                {
                    b.HasOne("OccBooking.Domain.Entities.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}
