using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class RenamedPKColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Receptionists",
                newName: "ReceptionistId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Patients",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LabManagers",
                newName: "LabManagerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LabAssistants",
                newName: "LabAssistantId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Doctors",
                newName: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceptionistId",
                table: "Receptionists",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "LabManagerId",
                table: "LabManagers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "LabAssistantId",
                table: "LabAssistants",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Doctors",
                newName: "UserId");
        }
    }
}
