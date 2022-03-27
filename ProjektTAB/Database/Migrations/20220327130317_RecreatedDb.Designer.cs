﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(ClinicContext))]
    [Migration("20220327130317_RecreatedDb")]
    partial class RecreatedDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Database.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<int?>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Database.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"), 1L, 1);

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorUserId")
                        .HasColumnType("int");

                    b.Property<int>("PatientUserId")
                        .HasColumnType("int");

                    b.Property<int>("ReceptionistUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorUserId");

                    b.HasIndex("PatientUserId");

                    b.HasIndex("ReceptionistUserId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Database.Examinations.ExaminationTemplate", b =>
                {
                    b.Property<string>("ExaminationCode")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("ExaminationType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExaminationCode");

                    b.ToTable("ExaminationTemplates");
                });

            modelBuilder.Entity("Database.Examinations.LabExamination", b =>
                {
                    b.Property<int>("LabExaminationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LabExaminationId"), 1L, 1);

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExaminationCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("ExaminationTemplateExaminationCode")
                        .HasColumnType("nvarchar(3)");

                    b.Property<DateTime?>("ExecutionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LabAssistantUserId")
                        .HasColumnType("int");

                    b.Property<string>("LabManagerComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LabManagerUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("LabExaminationId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("ExaminationTemplateExaminationCode");

                    b.HasIndex("LabAssistantUserId");

                    b.HasIndex("LabManagerUserId");

                    b.ToTable("LabExaminations");
                });

            modelBuilder.Entity("Database.Examinations.PhysicalExamination", b =>
                {
                    b.Property<int>("PhysicalExaminationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhysicalExaminationId"), 1L, 1);

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<string>("ExaminationCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("ExaminationTemplateExaminationCode")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhysicalExaminationId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("ExaminationTemplateExaminationCode");

                    b.ToTable("PhysicalExaminations");
                });

            modelBuilder.Entity("Database.People.Doctor", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DoctorId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("LicenseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("DoctorId");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("Database.People.LabAssistant", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LabAssistantId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("LabAssistantId");

                    b.ToTable("LabAssistants", (string)null);
                });

            modelBuilder.Entity("Database.People.LabManager", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LabManagerId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("LabManagerId");

                    b.ToTable("LabManagers", (string)null);
                });

            modelBuilder.Entity("Database.People.Patient", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PatientId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("PatientId");

                    b.HasIndex("AddressId");

                    b.ToTable("Patients", (string)null);
                });

            modelBuilder.Entity("Database.People.Receptionist", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReceptionistId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("ReceptionistId");

                    b.ToTable("Receptionists", (string)null);
                });

            modelBuilder.Entity("Database.Appointment", b =>
                {
                    b.HasOne("Database.People.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.People.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.People.Receptionist", "Receptionist")
                        .WithMany()
                        .HasForeignKey("ReceptionistUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Receptionist");
                });

            modelBuilder.Entity("Database.Examinations.LabExamination", b =>
                {
                    b.HasOne("Database.Appointment", "Appointment")
                        .WithMany("LabExaminations")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Examinations.ExaminationTemplate", "ExaminationTemplate")
                        .WithMany()
                        .HasForeignKey("ExaminationTemplateExaminationCode");

                    b.HasOne("Database.People.LabAssistant", "LabAssistant")
                        .WithMany()
                        .HasForeignKey("LabAssistantUserId");

                    b.HasOne("Database.People.LabManager", "LabManager")
                        .WithMany()
                        .HasForeignKey("LabManagerUserId");

                    b.Navigation("Appointment");

                    b.Navigation("ExaminationTemplate");

                    b.Navigation("LabAssistant");

                    b.Navigation("LabManager");
                });

            modelBuilder.Entity("Database.Examinations.PhysicalExamination", b =>
                {
                    b.HasOne("Database.Appointment", "Appointment")
                        .WithMany("PhysicalExaminations")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Examinations.ExaminationTemplate", "ExaminationTemplate")
                        .WithMany()
                        .HasForeignKey("ExaminationTemplateExaminationCode");

                    b.Navigation("Appointment");

                    b.Navigation("ExaminationTemplate");
                });

            modelBuilder.Entity("Database.People.Patient", b =>
                {
                    b.HasOne("Database.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Database.Appointment", b =>
                {
                    b.Navigation("LabExaminations");

                    b.Navigation("PhysicalExaminations");
                });
#pragma warning restore 612, 618
        }
    }
}