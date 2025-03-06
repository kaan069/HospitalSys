using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hastane_Proj.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorTcToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorTc",
                table: "Doctors",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorTc",
                table: "Doctors");
        }
    }
}
