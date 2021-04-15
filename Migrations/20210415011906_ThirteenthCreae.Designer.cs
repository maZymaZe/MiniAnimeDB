﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniAnimeDB.Data;

namespace MiniAnimeDB.Migrations
{
    [DbContext(typeof(MiniAnimeDBContext))]
    [Migration("20210415011906_ThirteenthCreae")]
    partial class ThirteenthCreae
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MiniAnimeDB.Models.Anime", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Aired")
                        .HasColumnType("datetime2");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Ended")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Eps")
                        .HasColumnType("int");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Anime");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.AnimeCharacter", b =>
                {
                    b.Property<int>("AnimeCharacterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimeID")
                        .HasColumnType("int");

                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.HasKey("AnimeCharacterID");

                    b.HasIndex("AnimeID");

                    b.HasIndex("CharacterID");

                    b.ToTable("AnimeCharacter");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.AnimePerson", b =>
                {
                    b.Property<int>("AnimePersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimeID")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnimePersonID");

                    b.HasIndex("AnimeID");

                    b.HasIndex("PersonID");

                    b.ToTable("AnimePerson");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.AnimeTagA", b =>
                {
                    b.Property<int>("AnimeTagAID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimeID")
                        .HasColumnType("int");

                    b.Property<int>("TagAID")
                        .HasColumnType("int");

                    b.HasKey("AnimeTagAID");

                    b.HasIndex("AnimeID");

                    b.HasIndex("TagAID");

                    b.ToTable("AnimeTagA");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.Character", b =>
                {
                    b.Property<int>("CharacterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CharacterID");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.CharacterTagC", b =>
                {
                    b.Property<int>("CharacterTagCID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.Property<int>("TagCID")
                        .HasColumnType("int");

                    b.HasKey("CharacterTagCID");

                    b.HasIndex("CharacterID");

                    b.HasIndex("TagCID");

                    b.ToTable("CharacterTagC");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.PersonCharacter", b =>
                {
                    b.Property<int>("PersonCharacterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("PersonCharacterID");

                    b.HasIndex("CharacterID");

                    b.HasIndex("PersonID");

                    b.ToTable("PersonCharacter");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.TagA", b =>
                {
                    b.Property<int>("TagAID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagAID");

                    b.ToTable("TagA");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.TagC", b =>
                {
                    b.Property<int>("TagCID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagCID");

                    b.ToTable("TagC");
                });

            modelBuilder.Entity("MiniAnimeDB.Models.AnimeCharacter", b =>
                {
                    b.HasOne("MiniAnimeDB.Models.Anime", "Anime")
                        .WithMany("Roles")
                        .HasForeignKey("AnimeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniAnimeDB.Models.Character", "Character")
                        .WithMany("Roles")
                        .HasForeignKey("CharacterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MiniAnimeDB.Models.AnimePerson", b =>
                {
                    b.HasOne("MiniAnimeDB.Models.Anime", "Anime")
                        .WithMany("Staffs")
                        .HasForeignKey("AnimeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniAnimeDB.Models.Person", "Person")
                        .WithMany("Staffs")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MiniAnimeDB.Models.AnimeTagA", b =>
                {
                    b.HasOne("MiniAnimeDB.Models.Anime", "Anime")
                        .WithMany("WithTags")
                        .HasForeignKey("AnimeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniAnimeDB.Models.TagA", "TagA")
                        .WithMany("WithTags")
                        .HasForeignKey("TagAID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MiniAnimeDB.Models.CharacterTagC", b =>
                {
                    b.HasOne("MiniAnimeDB.Models.Character", "Character")
                        .WithMany("WithTags")
                        .HasForeignKey("CharacterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniAnimeDB.Models.TagC", "TagC")
                        .WithMany("WithTags")
                        .HasForeignKey("TagCID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MiniAnimeDB.Models.PersonCharacter", b =>
                {
                    b.HasOne("MiniAnimeDB.Models.Character", "Character")
                        .WithMany("Casts")
                        .HasForeignKey("CharacterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniAnimeDB.Models.Person", "Person")
                        .WithMany("Casts")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
