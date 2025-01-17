﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FloraEducationAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FloraEducationAPI.Migrations
{
    [DbContext(typeof(FloraEducationDbContext))]
    [Migration("20220830190140_RenameFKForPlantsInCommentsTable")]
    partial class RenameFKForPlantsInCommentsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Authentication.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Badge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorUsername")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<Guid?>("PlantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorUsername");

                    b.HasIndex("PlantId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.MiniQuiz", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlantId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlantId");

                    b.ToTable("MiniQuiz");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Plant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Maintenance")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Planting")
                        .HasColumnType("text");

                    b.Property<string>("Predispositions")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.QuizQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Answers")
                        .HasColumnType("text[]");

                    b.Property<int>("CorrectAnswerIndex")
                        .HasColumnType("integer");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<Guid?>("QuizId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("QuizQuestion");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Relations.UserBadges", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BadgeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BadgeId");

                    b.HasIndex("Username");

                    b.ToTable("UserBadges");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Comment", b =>
                {
                    b.HasOne("FloraEducationAPI.Domain.Models.Authentication.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUsername");

                    b.HasOne("FloraEducationAPI.Domain.Models.Plant", "Plant")
                        .WithMany("Comments")
                        .HasForeignKey("PlantId")
                        .HasConstraintName("FK_PlantId");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.MiniQuiz", b =>
                {
                    b.HasOne("FloraEducationAPI.Domain.Models.Plant", "Plant")
                        .WithMany()
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.QuizQuestion", b =>
                {
                    b.HasOne("FloraEducationAPI.Domain.Models.MiniQuiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Relations.UserBadges", b =>
                {
                    b.HasOne("FloraEducationAPI.Domain.Models.Badge", "Badge")
                        .WithMany("Users")
                        .HasForeignKey("BadgeId")
                        .HasConstraintName("FK_BadgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FloraEducationAPI.Domain.Models.Authentication.User", "User")
                        .WithMany("Badges")
                        .HasForeignKey("Username")
                        .HasConstraintName("FK_Username");
                });
#pragma warning restore 612, 618
        }
    }
}
