﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using source.Models;

#nullable disable

namespace source.Migrations
{
    [DbContext(typeof(TravelContext))]
    [Migration("20230226031615___init__orderTran")]
    partial class initorderTran
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("source.Models.Account", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Roleid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("createdAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hashPassword")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.HasIndex("Roleid");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("source.Models.CategoryTour", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("CategoryTours");
                });

            modelBuilder.Entity("source.Models.Hotel", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<DateTime?>("createdAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("departureSchedule")
                        .HasColumnType("ntext");

                    b.Property<string>("desc")
                        .HasColumnType("ntext");

                    b.Property<string>("info")
                        .HasColumnType("ntext");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mainImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("openTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("schedule")
                        .HasColumnType("ntext");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("source.Models.HotelImg", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Hotelid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("alt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("src")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Hotelid");

                    b.ToTable("HotelImages");
                });

            modelBuilder.Entity("source.Models.OrderHotel", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Hotelid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("adultCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("childrenCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createdAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Hotelid");

                    b.ToTable("OrderHotels");
                });

            modelBuilder.Entity("source.Models.OrderTour", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("Tourid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("adultCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("childrenCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createdAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Tourid");

                    b.ToTable("OrderTours");
                });

            modelBuilder.Entity("source.Models.OrderTransport", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("Transportid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("adultCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("childrenCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createdAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Transportid");

                    b.ToTable("OrderTransports");
                });

            modelBuilder.Entity("source.Models.Role", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("source.Models.Tour", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("categoryTourid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("createdAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("departureSchedule")
                        .HasColumnType("ntext");

                    b.Property<string>("desc")
                        .HasColumnType("ntext");

                    b.Property<string>("info")
                        .HasColumnType("ntext");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mainImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("openTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("schedule")
                        .HasColumnType("ntext");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("categoryTourid");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("source.Models.TourImage", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Tourid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("alt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("src")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Tourid");

                    b.ToTable("TourImages");
                });

            modelBuilder.Entity("source.Models.Transport", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<DateTime?>("createdAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("departureSchedule")
                        .HasColumnType("ntext");

                    b.Property<string>("desc")
                        .HasColumnType("ntext");

                    b.Property<string>("info")
                        .HasColumnType("ntext");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mainImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("openTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("schedule")
                        .HasColumnType("ntext");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("source.Models.TransportImage", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Transportid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("alt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("src")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Transportid");

                    b.ToTable("TransportImages");
                });

            modelBuilder.Entity("source.Models.Account", b =>
                {
                    b.HasOne("source.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("Roleid");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("source.Models.HotelImg", b =>
                {
                    b.HasOne("source.Models.Hotel", "Hotel")
                        .WithMany("HotelImgs")
                        .HasForeignKey("Hotelid");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("source.Models.OrderHotel", b =>
                {
                    b.HasOne("source.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("Hotelid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("source.Models.OrderTour", b =>
                {
                    b.HasOne("source.Models.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("Tourid");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("source.Models.OrderTransport", b =>
                {
                    b.HasOne("source.Models.Transport", "Transport")
                        .WithMany()
                        .HasForeignKey("Transportid");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("source.Models.Tour", b =>
                {
                    b.HasOne("source.Models.CategoryTour", "categoryTour")
                        .WithMany()
                        .HasForeignKey("categoryTourid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoryTour");
                });

            modelBuilder.Entity("source.Models.TourImage", b =>
                {
                    b.HasOne("source.Models.Tour", "Tour")
                        .WithMany("TourImages")
                        .HasForeignKey("Tourid");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("source.Models.TransportImage", b =>
                {
                    b.HasOne("source.Models.Transport", "Transport")
                        .WithMany("TransportImages")
                        .HasForeignKey("Transportid");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("source.Models.Hotel", b =>
                {
                    b.Navigation("HotelImgs");
                });

            modelBuilder.Entity("source.Models.Tour", b =>
                {
                    b.Navigation("TourImages");
                });

            modelBuilder.Entity("source.Models.Transport", b =>
                {
                    b.Navigation("TransportImages");
                });
#pragma warning restore 612, 618
        }
    }
}
