using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bravure.Migrations
{
    public partial class adddepartmentmedcaiassistancerelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MedicalAssistances_DepartmentId",
                table: "MedicalAssistances",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalAssistances_Departments_DepartmentId",
                table: "MedicalAssistances",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalAssistances_Departments_DepartmentId",
                table: "MedicalAssistances");

            migrationBuilder.DropIndex(
                name: "IX_MedicalAssistances_DepartmentId",
                table: "MedicalAssistances");
        }
    }
}
