﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    [DbContext(typeof(ArticleDbContext))]
    [Migration("20200412122728_DateTimeRequired")]
    partial class DateTimeRequired
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataModel.Data.Entities.Article", b =>
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
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.Property<long>("WriterId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("WriterId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("DataModel.Data.Entities.Comment", b =>
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
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

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

            modelBuilder.Entity("DataModel.Data.Entities.Reviewer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ReviewerDescription")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("ReviewerRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Reviewer");
                });

            modelBuilder.Entity("DataModel.Data.Entities.ReviewerTopic", b =>
                {
                    b.Property<long>("ReviewerId")
                        .HasColumnType("bigint");

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.HasKey("ReviewerId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("ReviewerTopic");
                });

            modelBuilder.Entity("DataModel.Data.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.HasKey("Id");

                    b.HasIndex("RoleName");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DataModel.Data.Entities.Topic", b =>
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

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("DataModel.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ProfilePickLink")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<long>("ReviewerId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("WriterId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("ReviewerId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("WriterId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataModel.Data.Entities.Writer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WriterDescription")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("WriterRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Writer");
                });

            modelBuilder.Entity("DataModel.Data.Entities.WriterTopic", b =>
                {
                    b.Property<long>("WriterId")
                        .HasColumnType("bigint");

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.HasKey("WriterId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("WriterTopic");
                });

            modelBuilder.Entity("DataModel.Data.Entities.Article", b =>
                {
                    b.HasOne("DataModel.Data.Entities.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModel.Data.Entities.Writer", "Writer")
                        .WithMany()
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModel.Data.Entities.Comment", b =>
                {
                    b.HasOne("DataModel.Data.Entities.Comment", "AnsweredComment")
                        .WithMany()
                        .HasForeignKey("AnsweredCommentId");

                    b.HasOne("DataModel.Data.Entities.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModel.Data.Entities.Reviewer", "Reviewer")
                        .WithMany()
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModel.Data.Entities.ReviewerTopic", b =>
                {
                    b.HasOne("DataModel.Data.Entities.Reviewer", "Reviewer")
                        .WithMany("TopicsLink")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModel.Data.Entities.Topic", "Topic")
                        .WithMany("ReviewersLink")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModel.Data.Entities.User", b =>
                {
                    b.HasOne("DataModel.Data.Entities.Reviewer", "Reviewer")
                        .WithOne("User")
                        .HasForeignKey("DataModel.Data.Entities.User", "ReviewerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModel.Data.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModel.Data.Entities.Writer", "Writer")
                        .WithOne("User")
                        .HasForeignKey("DataModel.Data.Entities.User", "WriterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModel.Data.Entities.WriterTopic", b =>
                {
                    b.HasOne("DataModel.Data.Entities.Topic", "Topic")
                        .WithMany("WritersLink")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModel.Data.Entities.Writer", "Writer")
                        .WithMany("TopicsLink")
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
