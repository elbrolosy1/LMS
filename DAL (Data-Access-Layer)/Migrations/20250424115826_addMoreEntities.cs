using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL__Data_Access_Layer_.Migrations
{
    /// <inheritdoc />
    public partial class addMoreEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BornYear",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeathYear",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BornYear",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeathYear",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Authors");
        }
    }
}
