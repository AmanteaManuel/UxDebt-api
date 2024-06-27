﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UxDebt.Context;

#nullable disable

namespace UxDebt.Migrations
{
    [DbContext(typeof(UxDebtContext))]
    partial class UxDebtContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UxDebt.Entities.Issue", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueId"), 1L, 1);

                    b.Property<DateTime?>("ClosedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Discarded")
                        .HasColumnType("bit");

                    b.Property<long>("GitId")
                        .HasColumnType("bigint");

                    b.Property<string>("HtmlUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Labels")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepositoryId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IssueId");

                    b.HasIndex("GitId")
                        .IsUnique();

                    b.HasIndex("RepositoryId");

                    b.ToTable("Issue", (string)null);
                });

            modelBuilder.Entity("UxDebt.Entities.Repository", b =>
                {
                    b.Property<int>("RepositoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RepositoryId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GitId")
                        .HasColumnType("int");

                    b.Property<string>("HtmlUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RepositoryId");

                    b.HasIndex("GitId")
                        .IsUnique();

                    b.ToTable("Repository", (string)null);
                });

            modelBuilder.Entity("UxDebt.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("UxDebt.Models.Entities.IssueTag", b =>
                {
                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("IssueTagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueTagId"), 1L, 1);

                    b.HasKey("IssueId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("IssueTags");
                });

            modelBuilder.Entity("UxDebt.Entities.Issue", b =>
                {
                    b.HasOne("UxDebt.Entities.Repository", "Repository")
                        .WithMany("Issues")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("UxDebt.Models.Entities.IssueTag", b =>
                {
                    b.HasOne("UxDebt.Entities.Issue", "Issue")
                        .WithMany("IssueTags")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UxDebt.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("UxDebt.Entities.Issue", b =>
                {
                    b.Navigation("IssueTags");
                });

            modelBuilder.Entity("UxDebt.Entities.Repository", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
