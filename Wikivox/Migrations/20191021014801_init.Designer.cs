﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wikivox.Data;

namespace Wikivox.Migrations
{
    [DbContext(typeof(WikivoxDbContext))]
    [Migration("20191021014801_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wikivox.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("YrReleased");

                    b.HasKey("Id");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("Wikivox.Models.AlbumSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId");

                    b.Property<int?>("ArtistId");

                    b.Property<int?>("SongId");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SongId");

                    b.ToTable("AlbumSong");
                });

            modelBuilder.Entity("Wikivox.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArtistName")
                        .IsRequired();

                    b.Property<string>("Bio");

                    b.Property<string>("HomeCountry");

                    b.Property<string>("HomeTown");

                    b.Property<int>("YrEnded");

                    b.Property<int>("YrFormed");

                    b.Property<int>("isActive");

                    b.HasKey("Id");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("Wikivox.Models.ArtistGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArtistId");

                    b.Property<int?>("GenreId");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("ArtistGenre");
                });

            modelBuilder.Entity("Wikivox.Models.ArtistMusician", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArtistId");

                    b.Property<int?>("MusicianId");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("MusicianId");

                    b.ToTable("ArtistMusician");
                });

            modelBuilder.Entity("Wikivox.Models.ArtistSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId");

                    b.Property<int?>("SongId");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("SongId");

                    b.ToTable("ArtistSong");
                });

            modelBuilder.Entity("Wikivox.Models.Entity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Entity");
                });

            modelBuilder.Entity("Wikivox.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("Wikivox.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EntityId");

                    b.Property<string>("ImgUrl");

                    b.Property<int>("PrimaryImage");

                    b.Property<int>("ResourceId");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Wikivox.Models.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Instrument");
                });

            modelBuilder.Entity("Wikivox.Models.Musician", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio");

                    b.Property<DateTime>("Birth");

                    b.Property<DateTime>("Death");

                    b.Property<string>("FirstName");

                    b.Property<string>("HomeCountry");

                    b.Property<string>("HomeTown");

                    b.Property<string>("LastName");

                    b.Property<string>("MusicianName");

                    b.Property<int>("isActive");

                    b.HasKey("Id");

                    b.ToTable("Musician");
                });

            modelBuilder.Entity("Wikivox.Models.MusicianInstrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("InstrumentId");

                    b.Property<int?>("MusicianId");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("MusicianId");

                    b.ToTable("MusicianInstrument");
                });

            modelBuilder.Entity("Wikivox.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Duration");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("Wikivox.Models.AlbumSong", b =>
                {
                    b.HasOne("Wikivox.Models.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId");

                    b.HasOne("Wikivox.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId");

                    b.HasOne("Wikivox.Models.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId");
                });

            modelBuilder.Entity("Wikivox.Models.ArtistGenre", b =>
                {
                    b.HasOne("Wikivox.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId");

                    b.HasOne("Wikivox.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("Wikivox.Models.ArtistMusician", b =>
                {
                    b.HasOne("Wikivox.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId");

                    b.HasOne("Wikivox.Models.Musician", "Musician")
                        .WithMany()
                        .HasForeignKey("MusicianId");
                });

            modelBuilder.Entity("Wikivox.Models.ArtistSong", b =>
                {
                    b.HasOne("Wikivox.Models.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId");

                    b.HasOne("Wikivox.Models.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId");
                });

            modelBuilder.Entity("Wikivox.Models.Image", b =>
                {
                    b.HasOne("Wikivox.Models.Entity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId");
                });

            modelBuilder.Entity("Wikivox.Models.MusicianInstrument", b =>
                {
                    b.HasOne("Wikivox.Models.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId");

                    b.HasOne("Wikivox.Models.Musician", "Musician")
                        .WithMany()
                        .HasForeignKey("MusicianId");
                });
#pragma warning restore 612, 618
        }
    }
}
