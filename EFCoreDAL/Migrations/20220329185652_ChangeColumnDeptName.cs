using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreDAL.Migrations
{
    public partial class ChangeColumnDeptName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Departments",
                newName: "DeptName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeptName",
                table: "Departments",
                newName: "Name");
        }
    }
}
