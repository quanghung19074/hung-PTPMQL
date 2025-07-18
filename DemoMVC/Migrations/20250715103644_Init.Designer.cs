﻿// <auto-generated />
using DemoMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250715103644_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("MvcMovie.Models.Daily", b =>
                {
                    b.Property<string>("MaDaiLy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Diachi")
                        .HasColumnType("TEXT");

                    b.Property<string>("DienThoai")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaHTPP")
                        .HasColumnType("TEXT");

                    b.Property<string>("NguoiDaiDien")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenDaiLy")
                        .HasColumnType("TEXT");

                    b.HasKey("MaDaiLy");

                    b.ToTable("Dailys");
                });

            modelBuilder.Entity("MvcMovie.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MvcMovie.Models.HeThongPhanPhoi", b =>
                {
                    b.Property<string>("MaDaiLy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Diachi")
                        .HasColumnType("TEXT");

                    b.Property<string>("DienThoai")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaHTPP")
                        .HasColumnType("TEXT");

                    b.Property<string>("NguoiDaiDien")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenDaiLy")
                        .HasColumnType("TEXT");

                    b.HasKey("MaDaiLy");

                    b.ToTable("HeThongPhanPhois");
                });

            modelBuilder.Entity("MvcMovie.Models.Person", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
