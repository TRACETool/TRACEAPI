﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SecurityWebhook.Lib.Repository;

#nullable disable

namespace SecurityWebhook.Lib.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241103024448_AddTables")]
    partial class AddTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.Anomalies", b =>
                {
                    b.Property<long>("AnomaliesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("AnomaliesId"));

                    b.Property<string>("ActionTaken")
                        .HasColumnType("text");

                    b.Property<int>("AnomalyType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrentHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("PreviousHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("RepoId")
                        .HasColumnType("bigint");

                    b.Property<int>("Severity")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("AnomaliesId");

                    b.HasIndex("RepoId");

                    b.HasIndex("UserId");

                    b.ToTable("Anomalies");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ContributorMaster", b =>
                {
                    b.Property<long>("ContributorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ContributorId"));

                    b.Property<string>("ContributorEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContributorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ContributorId");

                    b.ToTable("ContributorMaster");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ContributorRepositories", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ContributorId")
                        .HasColumnType("bigint");

                    b.Property<long>("RepositoryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ContributorId");

                    b.HasIndex("RepositoryId");

                    b.ToTable("ContributorRepositories");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ImmutableServiceLogs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CurrentHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PreviousHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Repository")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ImmutableServiceLogs");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.RepositoryMaster", b =>
                {
                    b.Property<long>("RepoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("RepoId"));

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RepoName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RepoUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RepoId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("RepositoryMaster");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.RoleMaster", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("RoleMaster");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ScanDetails", b =>
                {
                    b.Property<long>("ScanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ScanId"));

                    b.Property<string>("CurrentHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Event")
                        .HasColumnType("integer");

                    b.Property<string>("PreviousHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RawData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RedMarked")
                        .HasColumnType("integer");

                    b.Property<long>("RepoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ScanTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TimeElapsed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TotalVulnerabilities")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("WhiteMarked")
                        .HasColumnType("integer");

                    b.Property<int>("YellowMarked")
                        .HasColumnType("integer");

                    b.HasKey("ScanId");

                    b.HasIndex("RepoId");

                    b.HasIndex("UserId");

                    b.ToTable("ScanDetails");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ScanFrequencyMaster", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Events")
                        .HasColumnType("integer");

                    b.Property<long>("RepoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RepoId");

                    b.ToTable("ScanFrequencyMaster");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.Vulnerabilities", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CurrentHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("DetectionTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PreviousHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("RepoId")
                        .HasColumnType("bigint");

                    b.Property<int>("Severity")
                        .HasColumnType("integer");

                    b.Property<string>("Snapshot")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RepoId");

                    b.HasIndex("UserId");

                    b.ToTable("Vulnerabilities");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.Anomalies", b =>
                {
                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.RepositoryMaster", "Repository")
                        .WithMany("Anomalies")
                        .HasForeignKey("RepoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.ContributorMaster", "Contributor")
                        .WithMany("Anomalies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contributor");

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ContributorRepositories", b =>
                {
                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.ContributorMaster", "Contributor")
                        .WithMany("ContributorRepositories")
                        .HasForeignKey("ContributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.RepositoryMaster", "Repository")
                        .WithMany("ContributorRepositories")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contributor");

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.RepositoryMaster", b =>
                {
                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.ContributorMaster", "Owner")
                        .WithMany("Repositories")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ScanDetails", b =>
                {
                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.RepositoryMaster", "Repository")
                        .WithMany("ScanDetails")
                        .HasForeignKey("RepoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.ContributorMaster", "Contributor")
                        .WithMany("ScanDetails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contributor");

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ScanFrequencyMaster", b =>
                {
                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.RepositoryMaster", "Repository")
                        .WithMany("ScanFrequencyMaster")
                        .HasForeignKey("RepoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.Vulnerabilities", b =>
                {
                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.RepositoryMaster", "Repository")
                        .WithMany("Vulnerabilities")
                        .HasForeignKey("RepoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecurityWebhook.Lib.Repository.Entities.ContributorMaster", "Contributor")
                        .WithMany("Vulnerabilities")
                        .HasForeignKey("UserId");

                    b.Navigation("Contributor");

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.ContributorMaster", b =>
                {
                    b.Navigation("Anomalies");

                    b.Navigation("ContributorRepositories");

                    b.Navigation("Repositories");

                    b.Navigation("ScanDetails");

                    b.Navigation("Vulnerabilities");
                });

            modelBuilder.Entity("SecurityWebhook.Lib.Repository.Entities.RepositoryMaster", b =>
                {
                    b.Navigation("Anomalies");

                    b.Navigation("ContributorRepositories");

                    b.Navigation("ScanDetails");

                    b.Navigation("ScanFrequencyMaster");

                    b.Navigation("Vulnerabilities");
                });
#pragma warning restore 612, 618
        }
    }
}
