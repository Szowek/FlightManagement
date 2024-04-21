﻿// <auto-generated />
using System;
using FlightManagement.Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightManagement.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240421183915_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("FlightManagement.Entities.ModelEntities.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AircraftType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ArrivalLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartureLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}
