﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tutorly.Infrastructure;

#nullable disable

namespace Tutorly.Infrastructure.Migrations
{
    [DbContext(typeof(TutorlyDbContext))]
    partial class TutorlyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tutorly.Domain.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("HappensAt")
                        .HasColumnType("time");

                    b.Property<int>("HappensOn")
                        .HasColumnType("int");

                    b.Property<bool>("IsHappeningAtStudentPlace")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemotely")
                        .HasColumnType("bit");

                    b.Property<int>("MaxStudentAmount")
                        .HasColumnType("int");

                    b.Property<int>("StudentsGrade")
                        .HasColumnType("int");

                    b.Property<int>("TutorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.HasIndex("TutorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.PostsStudents", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.HasKey("PostId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("PostsStudents");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<int>("Role");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Student", b =>
                {
                    b.HasBaseType("Tutorly.Domain.Models.User");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Tutor", b =>
                {
                    b.HasBaseType("Tutorly.Domain.Models.User");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Experience")
                        .HasColumnType("tinyint");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Post", b =>
                {
                    b.HasOne("Tutorly.Domain.Models.Address", "Address")
                        .WithOne("Post")
                        .HasForeignKey("Tutorly.Domain.Models.Post", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tutorly.Domain.Models.Category", "Category")
                        .WithOne("Post")
                        .HasForeignKey("Tutorly.Domain.Models.Post", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tutorly.Domain.Models.Tutor", "Tutor")
                        .WithMany("Posts")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Category");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.PostsStudents", b =>
                {
                    b.HasOne("Tutorly.Domain.Models.Post", "Post")
                        .WithMany("Students")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tutorly.Domain.Models.Student", "Student")
                        .WithMany("Posts")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Address", b =>
                {
                    b.Navigation("Post");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Category", b =>
                {
                    b.Navigation("Post");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Post", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Student", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Tutorly.Domain.Models.Tutor", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
