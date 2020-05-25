﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI;

namespace WebAPI.Migrations
{
    [DbContext(typeof(ArticleDbContext))]
    [Migration("20200511131523_RatingIsComputed")]
    partial class RatingIsComputed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebArticles.DataModel.Entities.Article", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Overview")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("PublichDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.Property<long>("WriterId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("WriterId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AnsweredCommentId")
                        .HasColumnType("bigint");

                    b.Property<long>("ArticleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublichDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<long>("ReviewerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AnsweredCommentId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Reviewer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ReviewerDescription")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Reviewer");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.ReviewerTopic", b =>
                {
                    b.Property<long>("ReviewerId")
                        .HasColumnType("bigint");

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.HasKey("ReviewerId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("ReviewerTopic");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles","Auth");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Topic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.HasKey("Id");

                    b.HasIndex("TopicName");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePickLink")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users","Auth");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Writer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("WriterDescription")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Writer");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.WriterTopic", b =>
                {
                    b.Property<long>("WriterId")
                        .HasColumnType("bigint");

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.HasKey("WriterId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("WriterTopic");
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims","Auth");
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims","Auth");
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins","Auth");
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole","Auth");
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.UserToken", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens","Auth");
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Article", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebArticles.DataModel.Entities.Writer", "Writer")
                        .WithMany("Articles")
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Comment", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.Comment", "AnsweredComment")
                        .WithMany()
                        .HasForeignKey("AnsweredCommentId");

                    b.HasOne("WebArticles.DataModel.Entities.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebArticles.DataModel.Entities.Reviewer", "Reviewer")
                        .WithMany("Comments")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Reviewer", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.User", "User")
                        .WithOne("Reviewer")
                        .HasForeignKey("WebArticles.DataModel.Entities.Reviewer", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.ReviewerTopic", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.Reviewer", "Reviewer")
                        .WithMany("TopicsLink")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebArticles.DataModel.Entities.Topic", "Topic")
                        .WithMany("ReviewersLink")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.Writer", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.User", "User")
                        .WithOne("Writer")
                        .HasForeignKey("WebArticles.DataModel.Entities.Writer", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.DataModel.Entities.WriterTopic", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.Topic", "Topic")
                        .WithMany("WritersLink")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebArticles.DataModel.Entities.Writer", "Writer")
                        .WithMany("TopicsLink")
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.RoleClaim", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.UserClaim", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.UserLogin", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.UserRole", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebArticles.DataModel.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebArticles.WebArticles.DataModel.Entities.Auth.UserToken", b =>
                {
                    b.HasOne("WebArticles.DataModel.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
