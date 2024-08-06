﻿// <auto-generated />
using System;
using Duende.Bff.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Tranchy.WebBff.Migrations.UserSessions
{
    [DbContext(typeof(SessionDbContext))]
    [Migration("20240806010738_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Duende.Bff.EntityFramework.UserSessionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ApplicationName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Renewed")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Ticket")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Expires");

                    b.HasIndex("ApplicationName", "Key")
                        .IsUnique()
                        .HasFilter("[ApplicationName] IS NOT NULL");

                    b.HasIndex("ApplicationName", "SessionId")
                        .IsUnique()
                        .HasFilter("[ApplicationName] IS NOT NULL AND [SessionId] IS NOT NULL");

                    b.HasIndex("ApplicationName", "SubjectId", "SessionId")
                        .IsUnique()
                        .HasFilter("[ApplicationName] IS NOT NULL AND [SessionId] IS NOT NULL");

                    b.ToTable("UserSessions", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
