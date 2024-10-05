using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalVolunteer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Species : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "species",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "breeds",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "species",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "breeds",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "species",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "breeds",
                newName: "title");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "species",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "breeds",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);
        }
    }
}
