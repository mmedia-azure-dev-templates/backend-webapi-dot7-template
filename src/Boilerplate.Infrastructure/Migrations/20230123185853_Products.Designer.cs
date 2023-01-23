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
    [Migration("20230123185853_Products")]
    partial class Products
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

            modelBuilder.Entity("Boilerplate.Domain.Entities.Identification", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("Address");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("BirthDate");

                    b.Property<int?>("CatCivilStatus")
                        .HasColumnType("integer")
                        .HasColumnName("CatCivilStatus");

                    b.Property<int?>("CatGender")
                        .HasColumnType("integer")
                        .HasColumnName("CatGender");

                    b.Property<int>("CatNacionality")
                        .HasColumnType("integer")
                        .HasColumnName("CatNacionality");

                    b.Property<int>("CatTypeDocument")
                        .HasColumnType("integer")
                        .HasColumnName("CatTypeDocument");

                    b.Property<string>("CurriculumUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("CurriculumUrl");

                    b.Property<DateOnly?>("DepartureDate")
                        .HasColumnType("date")
                        .HasColumnName("DepartureDate");

                    b.Property<DateOnly?>("EntryDate")
                        .HasColumnType("date")
                        .HasColumnName("EntryDate");

                    b.Property<short>("Hired")
                        .HasColumnType("smallint")
                        .HasColumnName("Hired");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("ImgUrl");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Mobile");

                    b.Property<string>("Ndocument")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("Ndocument")
                        .HasDefaultValueSql("'0'::bpchar");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Notes");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Phone");

                    b.Property<int?>("UbcCanton")
                        .HasColumnType("integer")
                        .HasColumnName("UbcCanton");

                    b.Property<int?>("UbcParroquia")
                        .HasColumnType("integer")
                        .HasColumnName("UbcParroquia");

                    b.Property<int?>("UbcProvincia")
                        .HasColumnType("integer")
                        .HasColumnName("UbcProvincia");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("UserId");

                    b.HasKey("Id")
                        .HasName("Identifications_Id");

                    b.HasIndex(new[] { "Ndocument" }, "Identifications_Ndocument_key")
                        .IsUnique();

                    b.HasIndex(new[] { "UserId" }, "Identifications_UserId_key")
                        .IsUnique();

                    b.ToTable("Identifications", "web", t =>
                        {
                            t.HasComment("TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO");
                        });
                });

            modelBuilder.Entity("Boilerplate.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

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

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("SurName");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("UserName");

                    b.HasKey("Id")
                        .HasName("Users_Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "Idx_Users_Name");

                    b.HasIndex(new[] { "SurName" }, "Idx_Users_SurName");

                    b.HasIndex(new[] { "Email" }, "Users_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "UserName" }, "Users_UserName")
                        .IsUnique();

                    b.ToTable("Users", "web");
                });
#pragma warning restore 612, 618
        }
    }
}
