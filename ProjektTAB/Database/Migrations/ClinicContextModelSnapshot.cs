﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(ClinicContext))]
    partial class ClinicContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("ReceptionistUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorUserId");

                    b.HasIndex("PatientId");

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

            modelBuilder.Entity("Database.Patients.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("RoomNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Database.Patients.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId");

                    b.HasIndex("AddressId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Database.Users.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserAccountId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.Users.UserAccount", b =>
                {
                    b.Property<int>("UserAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserAccountId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiries")
                        .HasColumnType("datetime2");

                    b.HasKey("UserAccountId");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Database.Users.Admin", b =>
                {
                    b.HasBaseType("Database.Users.User");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("Database.Users.Doctor", b =>
                {
                    b.HasBaseType("Database.Users.User");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("Database.Users.LabAssistant", b =>
                {
                    b.HasBaseType("Database.Users.User");

                    b.ToTable("LabAssistants", (string)null);
                });

            modelBuilder.Entity("Database.Users.LabManager", b =>
                {
                    b.HasBaseType("Database.Users.User");

                    b.ToTable("LabManagers", (string)null);
                });

            modelBuilder.Entity("Database.Users.Receptionist", b =>
                {
                    b.HasBaseType("Database.Users.User");

                    b.ToTable("Receptionists", (string)null);
                });

            modelBuilder.Entity("Database.Appointment", b =>
                {
                    b.HasOne("Database.Users.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Patients.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Users.Receptionist", "Receptionist")
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

                    b.HasOne("Database.Users.LabAssistant", "LabAssistant")
                        .WithMany()
                        .HasForeignKey("LabAssistantUserId");

                    b.HasOne("Database.Users.LabManager", "LabManager")
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

            modelBuilder.Entity("Database.Patients.Patient", b =>
                {
                    b.HasOne("Database.Patients.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Database.Users.User", b =>
                {
                    b.HasOne("Database.Users.UserAccount", "UserAccount")
                        .WithOne("User")
                        .HasForeignKey("Database.Users.User", "UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Database.Users.Admin", b =>
                {
                    b.HasOne("Database.Users.User", null)
                        .WithOne()
                        .HasForeignKey("Database.Users.Admin", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Users.Doctor", b =>
                {
                    b.HasOne("Database.Users.User", null)
                        .WithOne()
                        .HasForeignKey("Database.Users.Doctor", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Users.LabAssistant", b =>
                {
                    b.HasOne("Database.Users.User", null)
                        .WithOne()
                        .HasForeignKey("Database.Users.LabAssistant", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Users.LabManager", b =>
                {
                    b.HasOne("Database.Users.User", null)
                        .WithOne()
                        .HasForeignKey("Database.Users.LabManager", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Users.Receptionist", b =>
                {
                    b.HasOne("Database.Users.User", null)
                        .WithOne()
                        .HasForeignKey("Database.Users.Receptionist", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Appointment", b =>
                {
                    b.Navigation("LabExaminations");

                    b.Navigation("PhysicalExaminations");
                });

            modelBuilder.Entity("Database.Users.UserAccount", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
