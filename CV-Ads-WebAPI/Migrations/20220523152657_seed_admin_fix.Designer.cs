﻿// <auto-generated />
using System;
using CV_Ads_WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CV_Ads_WebAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220523152657_seed_admin_fix")]
    partial class seed_admin_fix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"),
                            FirstName = "Default",
                            LastName = "Admin"
                        });
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.Advertisement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CityScope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryScope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("ViewsLimit")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.AdvertisementView", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdvertisementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AudienceCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("SmartDeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.HasIndex("SmartDeviceId");

                    b.ToTable("AdvertisementViews");
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastPaidDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.HumanLimit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdvertisementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("MaxAge")
                        .HasColumnType("int");

                    b.Property<int>("MinAge")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.ToTable("HumanLimits");
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastWithdrawedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.SmartDevice", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCaching")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTurnedOn")
                        .HasColumnType("bit");

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<Guid?>("PartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.ToTable("SmartDevices");
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.TimePeriodLimit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdvertisementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FromInMinutes")
                        .HasColumnType("int");

                    b.Property<int>("ToInMinutes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.ToTable("TimePeriodLimits");
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.UserIdentity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("UserIdentities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"),
                            Login = "adam@nure.ua",
                            Password = "YVFjQHffGy3JitvNiD7sfE+NwgUesCXVH3zzpJ1HqVNUi2soi7DFh5T8PRu1dtXJ",
                            Role = "ADMIN"
                        });
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.Admin", b =>
                {
                    b.HasOne("CV_Ads_WebAPI.Domain.Models.UserIdentity", "UserIdentity")
                        .WithOne()
                        .HasForeignKey("CV_Ads_WebAPI.Domain.Models.Admin", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.Advertisement", b =>
                {
                    b.HasOne("CV_Ads_WebAPI.Domain.Models.Customer", "Customer")
                        .WithMany("Advertisements")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.AdvertisementView", b =>
                {
                    b.HasOne("CV_Ads_WebAPI.Domain.Models.Advertisement", "Advertisement")
                        .WithMany("AdvertisementViews")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CV_Ads_WebAPI.Domain.Models.SmartDevice", "SmartDevice")
                        .WithMany("AdvertisementViews")
                        .HasForeignKey("SmartDeviceId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.Customer", b =>
                {
                    b.HasOne("CV_Ads_WebAPI.Domain.Models.UserIdentity", "UserIdentity")
                        .WithOne()
                        .HasForeignKey("CV_Ads_WebAPI.Domain.Models.Customer", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.HumanLimit", b =>
                {
                    b.HasOne("CV_Ads_WebAPI.Domain.Models.Advertisement", "Advertisement")
                        .WithMany("HumanLimits")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.Partner", b =>
                {
                    b.HasOne("CV_Ads_WebAPI.Domain.Models.UserIdentity", "UserIdentity")
                        .WithOne()
                        .HasForeignKey("CV_Ads_WebAPI.Domain.Models.Partner", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.SmartDevice", b =>
                {
                    b.HasOne("CV_Ads_WebAPI.Domain.Models.UserIdentity", "UserIdentity")
                        .WithOne()
                        .HasForeignKey("CV_Ads_WebAPI.Domain.Models.SmartDevice", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CV_Ads_WebAPI.Domain.Models.Partner", "Partner")
                        .WithMany("SmartDevices")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("CV_Ads_WebAPI.Domain.Models.TimePeriodLimit", b =>
                {
                    b.HasOne("CV_Ads_WebAPI.Domain.Models.Advertisement", "Advertisement")
                        .WithMany("TimePeriodLimits")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
