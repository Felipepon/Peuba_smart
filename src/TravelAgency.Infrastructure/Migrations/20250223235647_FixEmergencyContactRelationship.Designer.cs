﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TravelAgency.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250223235647_FixEmergencyContactRelationship")]
    partial class FixEmergencyContactRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Hotel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("RoomId1")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("RoomId1");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.EmergencyContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("EmergencyContacts");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.Guest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("char(36)");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("BaseCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MaxGuests")
                        .HasColumnType("int");

                    b.Property<decimal>("Taxes")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.Booking", b =>
                {
                    b.HasOne("TravelAgency.Domain.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgency.Domain.Entities.Room", null)
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId1");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.EmergencyContact", b =>
                {
                    b.HasOne("TravelAgency.Domain.Entities.Booking", "Booking")
                        .WithOne("EmergencyContact")
                        .HasForeignKey("TravelAgency.Domain.Entities.EmergencyContact", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.Guest", b =>
                {
                    b.HasOne("TravelAgency.Domain.Entities.Booking", "Booking")
                        .WithMany("Guests")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.Room", b =>
                {
                    b.HasOne("Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.Booking", b =>
                {
                    b.Navigation("EmergencyContact")
                        .IsRequired();

                    b.Navigation("Guests");
                });

            modelBuilder.Entity("TravelAgency.Domain.Entities.Room", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
