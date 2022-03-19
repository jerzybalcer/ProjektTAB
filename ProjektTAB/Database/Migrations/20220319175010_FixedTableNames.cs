using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class FixedTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctor_DoctorUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Receptionist_ReceptionistUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Users_UserId",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_LabAssistant_Users_UserId",
                table: "LabAssistant");

            migrationBuilder.DropForeignKey(
                name: "FK_LabExaminations_LabAssistant_LabAssistantUserId",
                table: "LabExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_LabExaminations_LabManager_LabManagerUserId",
                table: "LabExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_LabManager_Users_UserId",
                table: "LabManager");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptionist_Users_UserId",
                table: "Receptionist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receptionist",
                table: "Receptionist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabManager",
                table: "LabManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabAssistant",
                table: "LabAssistant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.RenameTable(
                name: "Receptionist",
                newName: "Receptionists");

            migrationBuilder.RenameTable(
                name: "LabManager",
                newName: "LabManagers");

            migrationBuilder.RenameTable(
                name: "LabAssistant",
                newName: "LabAssistants");

            migrationBuilder.RenameTable(
                name: "Doctor",
                newName: "Doctors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receptionists",
                table: "Receptionists",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabManagers",
                table: "LabManagers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabAssistants",
                table: "LabAssistants",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorUserId",
                table: "Appointments",
                column: "DoctorUserId",
                principalTable: "Doctors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Receptionists_ReceptionistUserId",
                table: "Appointments",
                column: "ReceptionistUserId",
                principalTable: "Receptionists",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserId",
                table: "Doctors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabAssistants_Users_UserId",
                table: "LabAssistants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabExaminations_LabAssistants_LabAssistantUserId",
                table: "LabExaminations",
                column: "LabAssistantUserId",
                principalTable: "LabAssistants",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabExaminations_LabManagers_LabManagerUserId",
                table: "LabExaminations",
                column: "LabManagerUserId",
                principalTable: "LabManagers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabManagers_Users_UserId",
                table: "LabManagers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptionists_Users_UserId",
                table: "Receptionists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Receptionists_ReceptionistUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_LabAssistants_Users_UserId",
                table: "LabAssistants");

            migrationBuilder.DropForeignKey(
                name: "FK_LabExaminations_LabAssistants_LabAssistantUserId",
                table: "LabExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_LabExaminations_LabManagers_LabManagerUserId",
                table: "LabExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_LabManagers_Users_UserId",
                table: "LabManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptionists_Users_UserId",
                table: "Receptionists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receptionists",
                table: "Receptionists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabManagers",
                table: "LabManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabAssistants",
                table: "LabAssistants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Receptionists",
                newName: "Receptionist");

            migrationBuilder.RenameTable(
                name: "LabManagers",
                newName: "LabManager");

            migrationBuilder.RenameTable(
                name: "LabAssistants",
                newName: "LabAssistant");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "Doctor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receptionist",
                table: "Receptionist",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabManager",
                table: "LabManager",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabAssistant",
                table: "LabAssistant",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctor_DoctorUserId",
                table: "Appointments",
                column: "DoctorUserId",
                principalTable: "Doctor",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Receptionist_ReceptionistUserId",
                table: "Appointments",
                column: "ReceptionistUserId",
                principalTable: "Receptionist",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Users_UserId",
                table: "Doctor",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabAssistant_Users_UserId",
                table: "LabAssistant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabExaminations_LabAssistant_LabAssistantUserId",
                table: "LabExaminations",
                column: "LabAssistantUserId",
                principalTable: "LabAssistant",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabExaminations_LabManager_LabManagerUserId",
                table: "LabExaminations",
                column: "LabManagerUserId",
                principalTable: "LabManager",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabManager_Users_UserId",
                table: "LabManager",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptionist_Users_UserId",
                table: "Receptionist",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
