﻿// <auto-generated />
using System;
using Boilerplate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230122165751_Identifications")]
    partial class Identifications
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("web")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Boilerplate.Domain.Entities.Hero", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("HeroType")
                        .HasColumnType("integer");

                    b.Property<string>("Individuality")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<string>("Team")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Heroes", "web");
                });

            modelBuilder.Entity("Boilerplate.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Apellidos");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DeletedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("Email");

                    b.Property<short>("IsActive")
                        .HasColumnType("smallint")
                        .HasColumnName("IsActive");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastLogin");

                    b.Property<string>("LastLoginIp")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("LastLoginIp")
                        .HasDefaultValueSql("NULL::character varying");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Nombres");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Password");

                    b.Property<string>("RememberToken")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("RememberToken")
                        .HasDefaultValueSql("NULL::character varying");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Username");

                    b.HasKey("Id")
                        .HasName("Users_Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex(new[] { "Apellidos" }, "Idx_Users_Apellidos");

                    b.HasIndex(new[] { "Nombres" }, "Idx_Users_Nombres");

                    b.HasIndex(new[] { "Email" }, "Users_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "Users_Username")
                        .IsUnique();

                    b.ToTable("Users", "web");
                });
#pragma warning restore 612, 618
        }
    }
}
