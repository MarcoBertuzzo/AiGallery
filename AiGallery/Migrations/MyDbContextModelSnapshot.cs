﻿// <auto-generated />
using System;
using AiGallery.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AiGallery.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("AiGallery.Data.DbEntities+Image", b =>
                {
                    b.Property<int>("StripId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description_Eng")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description_Ita")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StripId", "Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("AiGallery.Data.DbEntities+Strip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastView")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title_Eng")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title_Ita")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ViewsCounter")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Strip");
                });

            modelBuilder.Entity("AiGallery.Data.DbEntities+User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AiGallery.Data.DbEntities+UserImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UserImage");
                });
#pragma warning restore 612, 618
        }
    }
}
