using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class CreateClinicDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationTemplates",
                columns: table => new
                {
                    ExaminationCode = table.Column<int>(type: "int", nullable: false),
                    ExaminationType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationTemplates", x => x.ExaminationCode);
                });

            migrationBuilder.CreateTable(
                name: "LabAssistants",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabAssistants", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "LabManagers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabManagers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Receptionists",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptionists", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Patients_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceptionistUserId = table.Column<int>(type: "int", nullable: false),
                    DoctorUserId = table.Column<int>(type: "int", nullable: false),
                    PatientUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorUserId",
                        column: x => x.DoctorUserId,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientUserId",
                        column: x => x.PatientUserId,
                        principalTable: "Patients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Receptionists_ReceptionistUserId",
                        column: x => x.ReceptionistUserId,
                        principalTable: "Receptionists",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabExaminations",
                columns: table => new
                {
                    LabExaminationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DoctorComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabManagerComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    ExaminationCode = table.Column<int>(type: "int", nullable: false),
                    ExaminationTemplateExaminationCode = table.Column<int>(type: "int", nullable: false),
                    LabAssistantUserId = table.Column<int>(type: "int", nullable: true),
                    LabManagerUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabExaminations", x => x.LabExaminationId);
                    table.ForeignKey(
                        name: "FK_LabExaminations_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabExaminations_ExaminationTemplates_ExaminationTemplateExaminationCode",
                        column: x => x.ExaminationTemplateExaminationCode,
                        principalTable: "ExaminationTemplates",
                        principalColumn: "ExaminationCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabExaminations_LabAssistants_LabAssistantUserId",
                        column: x => x.LabAssistantUserId,
                        principalTable: "LabAssistants",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_LabExaminations_LabManagers_LabManagerUserId",
                        column: x => x.LabManagerUserId,
                        principalTable: "LabManagers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExaminations",
                columns: table => new
                {
                    PhysicalExaminationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    ExaminationCode = table.Column<int>(type: "int", nullable: false),
                    ExaminationTemplateExaminationCode = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExaminations", x => x.PhysicalExaminationId);
                    table.ForeignKey(
                        name: "FK_PhysicalExaminations_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhysicalExaminations_ExaminationTemplates_ExaminationTemplateExaminationCode",
                        column: x => x.ExaminationTemplateExaminationCode,
                        principalTable: "ExaminationTemplates",
                        principalColumn: "ExaminationCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorUserId",
                table: "Appointments",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientUserId",
                table: "Appointments",
                column: "PatientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ReceptionistUserId",
                table: "Appointments",
                column: "ReceptionistUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LabExaminations_AppointmentId",
                table: "LabExaminations",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LabExaminations_ExaminationTemplateExaminationCode",
                table: "LabExaminations",
                column: "ExaminationTemplateExaminationCode");

            migrationBuilder.CreateIndex(
                name: "IX_LabExaminations_LabAssistantUserId",
                table: "LabExaminations",
                column: "LabAssistantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LabExaminations_LabManagerUserId",
                table: "LabExaminations",
                column: "LabManagerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AddressId",
                table: "Patients",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExaminations_AppointmentId",
                table: "PhysicalExaminations",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExaminations_ExaminationTemplateExaminationCode",
                table: "PhysicalExaminations",
                column: "ExaminationTemplateExaminationCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabExaminations");

            migrationBuilder.DropTable(
                name: "PhysicalExaminations");

            migrationBuilder.DropTable(
                name: "LabAssistants");

            migrationBuilder.DropTable(
                name: "LabManagers");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "ExaminationTemplates");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Receptionists");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
