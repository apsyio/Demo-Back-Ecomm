﻿// <auto-generated />
using System;
using Aps.Apps.CueTheCurves.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aps.Apps.CueTheCurves.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220214064745_1.1")]
    partial class _11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.BrandLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("UserId");

                    b.ToTable("BrandLikes");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Brands", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SizeTag")
                        .HasColumnType("bigint");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.ClosetItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClosetId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("XCoordinate")
                        .HasColumnType("bigint");

                    b.Property<long>("YCoordinate")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClosetId");

                    b.ToTable("ClosetItems");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Closets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("OutfitName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Closets");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.PostLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("PostsId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("PostsId");

                    b.HasIndex("UserId");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Posts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostType")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PosterId")
                        .HasColumnType("int");

                    b.Property<double>("SizeTag")
                        .HasColumnType("float");

                    b.Property<int?>("StyleId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("PosterId");

                    b.HasIndex("StyleId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Sizes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Tag")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.StyleBrands", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("StyleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("StyleId");

                    b.ToTable("StyleBrands");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.StyleLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.Property<int>("StyleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StyleId");

                    b.HasIndex("UserId");

                    b.ToTable("StyleLikes");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Styles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Colors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Styles");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.UserBrands", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBrands");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.UserSocials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("SocialNetworks")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSocials");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.UserStyles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("StyleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StyleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserStyles");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.BrandLikes", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Brands", "Brand")
                        .WithMany("BrandLikes")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", "User")
                        .WithMany("BrandLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.ClosetItems", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Closets", "Closet")
                        .WithMany("ClosetItems")
                        .HasForeignKey("ClosetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Closet");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Closets", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", "User")
                        .WithMany("Closets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.PostLikes", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Posts", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Posts", null)
                        .WithMany("PostLikes")
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", "User")
                        .WithMany("PostLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Posts", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Brands", "Brand")
                        .WithMany("Posts")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", "Poster")
                        .WithMany()
                        .HasForeignKey("PosterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Styles", "Style")
                        .WithMany("Posts")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Brand");

                    b.Navigation("Poster");

                    b.Navigation("Style");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.StyleBrands", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Brands", "Brand")
                        .WithMany("StyleBrands")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Styles", "Style")
                        .WithMany("StyleBrands")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Style");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.StyleLikes", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Styles", "Style")
                        .WithMany("StyleLikes")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", "User")
                        .WithMany("StyleLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Style");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.UserBrands", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Brands", "Brand")
                        .WithMany("UserBrands")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", "User")
                        .WithMany("UserBrands")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.UserSocials", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", "User")
                        .WithMany("Socials")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.UserStyles", b =>
                {
                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Styles", "Style")
                        .WithMany("UserStyles")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", "User")
                        .WithMany("UserStyles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Style");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Brands", b =>
                {
                    b.Navigation("BrandLikes");

                    b.Navigation("Posts");

                    b.Navigation("StyleBrands");

                    b.Navigation("UserBrands");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Closets", b =>
                {
                    b.Navigation("ClosetItems");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Posts", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("PostLikes");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Styles", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("StyleBrands");

                    b.Navigation("StyleLikes");

                    b.Navigation("UserStyles");
                });

            modelBuilder.Entity("Aps.Apps.CueTheCurves.Api.Models.Entities.Users", b =>
                {
                    b.Navigation("BrandLikes");

                    b.Navigation("Closets");

                    b.Navigation("PostLikes");

                    b.Navigation("Socials");

                    b.Navigation("StyleLikes");

                    b.Navigation("UserBrands");

                    b.Navigation("UserStyles");
                });
#pragma warning restore 612, 618
        }
    }
}
