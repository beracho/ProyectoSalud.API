﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProyectoSalud.API.Data;

namespace ProyectoSalud.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProyectoSalud.API.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Consultation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ConsultingRoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreationUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("MedicalHistoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ConsultingRoomId");

                    b.HasIndex("CreationUserId");

                    b.HasIndex("MedicalHistoryId");

                    b.HasIndex("UpdateUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.ConsultingRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreationUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("TelephoneId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreationUserId");

                    b.HasIndex("LocationId");

                    b.HasIndex("TelephoneId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("ConsultingRooms");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ConsultationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ConsultationId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Insure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreationUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsuranceDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("InsurerId")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InsurerId");

                    b.ToTable("Insures");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address1")
                        .HasColumnType("text");

                    b.Property<string>("Address2")
                        .HasColumnType("text");

                    b.Property<int>("CityAddressId")
                        .HasColumnType("integer");

                    b.Property<int>("CountryAddressId")
                        .HasColumnType("integer");

                    b.Property<string>("PostalCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityAddressId");

                    b.HasIndex("CountryAddressId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.MedicalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreationUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreationUserId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("MedicalHistories");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Ci")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreationUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ExpeditionCi")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<int?>("InsureId")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer");

                    b.Property<int?>("MedicalHistoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text");

                    b.Property<int?>("TelephoneId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InsureId");

                    b.HasIndex("LocationId");

                    b.HasIndex("MedicalHistoryId")
                        .IsUnique();

                    b.HasIndex("TelephoneId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AssociatedClass")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<int?>("PersonId")
                        .HasColumnType("integer");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<bool>("Default")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rols");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Specialitiess");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Telephone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<int>("CountryCode")
                        .HasColumnType("integer");

                    b.Property<int>("NationalNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Telephones");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreationUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("LoginAttemptCounter")
                        .HasColumnType("integer");

                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RecoveryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RecoveryKey")
                        .HasColumnType("text");

                    b.Property<int?>("SpecialityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserState")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("ValidationCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("ValidationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("VerifiedEmail")
                        .HasColumnType("boolean");

                    b.Property<string>("VerifiedEmailKey")
                        .HasColumnType("text");

                    b.Property<DateTime>("VerifyEmailDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.HasIndex("SpecialityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.UserRol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreationUserId")
                        .HasColumnType("integer");

                    b.Property<int>("RolId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdateUserId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRols");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.City", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Consultation", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.ConsultingRoom", "ConsultingRoom")
                        .WithMany()
                        .HasForeignKey("ConsultingRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.User", "CreationUser")
                        .WithMany()
                        .HasForeignKey("CreationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.MedicalHistory", "MedicalHistory")
                        .WithMany("Consultation")
                        .HasForeignKey("MedicalHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.User", "Doctor")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.ConsultingRoom", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.User", "CreationUser")
                        .WithMany()
                        .HasForeignKey("CreationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.Telephone", "Telephone")
                        .WithMany()
                        .HasForeignKey("TelephoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.File", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.Consultation", null)
                        .WithMany("Files")
                        .HasForeignKey("ConsultationId");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Insure", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.Insure", "Insurer")
                        .WithMany("Insurees")
                        .HasForeignKey("InsurerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Location", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.City", "CityAddress")
                        .WithMany()
                        .HasForeignKey("CityAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.Country", "CountryAddress")
                        .WithMany("LocationAddress")
                        .HasForeignKey("CountryAddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.MedicalHistory", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.User", "CreationUser")
                        .WithMany()
                        .HasForeignKey("CreationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Person", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.Insure", "Insure")
                        .WithMany()
                        .HasForeignKey("InsureId");

                    b.HasOne("ProyectoSalud.API.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("ProyectoSalud.API.Models.MedicalHistory", "MedicalHistory")
                        .WithOne("Patient")
                        .HasForeignKey("ProyectoSalud.API.Models.Person", "MedicalHistoryId");

                    b.HasOne("ProyectoSalud.API.Models.Telephone", "Telephone")
                        .WithMany()
                        .HasForeignKey("TelephoneId");
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.Photo", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.Person", null)
                        .WithMany("Photos")
                        .HasForeignKey("PersonId");

                    b.HasOne("ProyectoSalud.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.User", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.Person", "Person")
                        .WithOne("User")
                        .HasForeignKey("ProyectoSalud.API.Models.User", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.Speciality", "Speciality")
                        .WithMany("Users")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ProyectoSalud.API.Models.UserRol", b =>
                {
                    b.HasOne("ProyectoSalud.API.Models.Rol", "Rol")
                        .WithMany("UserRols")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoSalud.API.Models.User", "User")
                        .WithMany("UserRols")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
