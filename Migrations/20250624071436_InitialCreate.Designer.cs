﻿// <auto-generated />
using System;
using StudyTracker.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudyTracker.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250624071436_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StudyTracker.Model.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("StudyTracker.Model.ProjectUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ProjectIdId")
                        .HasColumnType("integer");

                    b.Property<int?>("RoleIdId")
                        .HasColumnType("integer");

                    b.Property<int>("UserIdId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectIdId");

                    b.HasIndex("RoleIdId");

                    b.HasIndex("UserIdId");

                    b.ToTable("ProjectUsers");
                });

            modelBuilder.Entity("StudyTracker.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProjectIdId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectIdId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("StudyTracker.Model.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ProjectIdId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectUserIdId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectIdId");

                    b.HasIndex("ProjectUserIdId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("StudyTracker.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StudyTracker.Model.Project", b =>
                {
                    b.HasOne("StudyTracker.Model.User", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("StudyTracker.Model.ProjectUser", b =>
                {
                    b.HasOne("StudyTracker.Model.Project", "ProjectId")
                        .WithMany()
                        .HasForeignKey("ProjectIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudyTracker.Model.Role", "RoleId")
                        .WithMany()
                        .HasForeignKey("RoleIdId");

                    b.HasOne("StudyTracker.Model.User", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectId");

                    b.Navigation("RoleId");

                    b.Navigation("UserId");
                });

            modelBuilder.Entity("StudyTracker.Model.Role", b =>
                {
                    b.HasOne("StudyTracker.Model.Project", "ProjectId")
                        .WithMany()
                        .HasForeignKey("ProjectIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectId");
                });

            modelBuilder.Entity("StudyTracker.Model.Task", b =>
                {
                    b.HasOne("StudyTracker.Model.Project", "ProjectId")
                        .WithMany()
                        .HasForeignKey("ProjectIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudyTracker.Model.ProjectUser", "ProjectUserId")
                        .WithMany()
                        .HasForeignKey("ProjectUserIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectId");

                    b.Navigation("ProjectUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
