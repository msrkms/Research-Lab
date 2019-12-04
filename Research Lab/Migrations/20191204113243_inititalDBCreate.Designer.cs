﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Research_Lab.Data;

namespace Research_Lab.Migrations
{
    [DbContext(typeof(ResearchLabContext))]
    [Migration("20191204113243_inititalDBCreate")]
    partial class inititalDBCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Research_Lab.Models.AppUser", b =>
                {
                    b.Property<int>("AppUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<bool>("IsVerified");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<int>("RoleId");

                    b.HasKey("AppUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("Research_Lab.Models.BookingInfo", b =>
                {
                    b.Property<int>("Biid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppUserId");

                    b.Property<DateTime>("BookingDate");

                    b.Property<DateTime>("BookingEndTime");

                    b.Property<DateTime>("BookingStartTime");

                    b.Property<int>("Cid");

                    b.HasKey("Biid");

                    b.HasIndex("AppUserId");

                    b.HasIndex("Cid");

                    b.ToTable("BookingInfo");
                });

            modelBuilder.Entity("Research_Lab.Models.Computer", b =>
                {
                    b.Property<int>("Cid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAvailable");

                    b.Property<int>("LabId");

                    b.HasKey("Cid");

                    b.HasIndex("LabId");

                    b.ToTable("Computer");
                });

            modelBuilder.Entity("Research_Lab.Models.LabCostRate", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Rlid");

                    b.Property<float>("costperminitue");

                    b.HasKey("id");

                    b.HasIndex("Rlid");

                    b.ToTable("LabCostRates");
                });

            modelBuilder.Entity("Research_Lab.Models.LabUseCost", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CId");

                    b.Property<DateTime>("UseDate");

                    b.Property<int>("appUserID");

                    b.Property<int>("hour");

                    b.Property<int>("minute");

                    b.Property<double>("totalCost");

                    b.HasKey("id");

                    b.HasIndex("CId");

                    b.HasIndex("appUserID");

                    b.ToTable("LabUseCosts");
                });

            modelBuilder.Entity("Research_Lab.Models.ResearchLab", b =>
                {
                    b.Property<int>("Rlid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LabAssistant");

                    b.Property<int?>("LabAssistantNavigationAppUserId");

                    b.Property<string>("LabLoction");

                    b.Property<string>("LabName");

                    b.Property<int?>("LabUseCostid");

                    b.HasKey("Rlid");

                    b.HasIndex("LabAssistantNavigationAppUserId");

                    b.HasIndex("LabUseCostid");

                    b.ToTable("ResearchLab");
                });

            modelBuilder.Entity("Research_Lab.Models.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleType");

                    b.HasKey("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Research_Lab.Models.AppUser", b =>
                {
                    b.HasOne("Research_Lab.Models.UserRole", "Role")
                        .WithMany("AppUser")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Research_Lab.Models.BookingInfo", b =>
                {
                    b.HasOne("Research_Lab.Models.AppUser", "AppUser")
                        .WithMany("BookingInfo")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Research_Lab.Models.Computer", "C")
                        .WithMany("BookingInfo")
                        .HasForeignKey("Cid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Research_Lab.Models.Computer", b =>
                {
                    b.HasOne("Research_Lab.Models.ResearchLab", "Lab")
                        .WithMany("Computer")
                        .HasForeignKey("LabId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Research_Lab.Models.LabCostRate", b =>
                {
                    b.HasOne("Research_Lab.Models.ResearchLab", "ResearchLab")
                        .WithMany()
                        .HasForeignKey("Rlid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Research_Lab.Models.LabUseCost", b =>
                {
                    b.HasOne("Research_Lab.Models.Computer", "Computer")
                        .WithMany("LabUseCosts")
                        .HasForeignKey("CId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Research_Lab.Models.AppUser", "appUser")
                        .WithMany("LabUseCost")
                        .HasForeignKey("appUserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Research_Lab.Models.ResearchLab", b =>
                {
                    b.HasOne("Research_Lab.Models.AppUser", "LabAssistantNavigation")
                        .WithMany("ResearchLab")
                        .HasForeignKey("LabAssistantNavigationAppUserId");

                    b.HasOne("Research_Lab.Models.LabUseCost", "LabUseCost")
                        .WithMany()
                        .HasForeignKey("LabUseCostid");
                });
#pragma warning restore 612, 618
        }
    }
}
