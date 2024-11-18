using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalVolunteer.Discussions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Discussions_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "discussions");

            migrationBuilder.CreateTable(
                name: "discussions",
                schema: "discussions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    users_ids = table.Column<Guid[]>(type: "uuid[]", nullable: false),
                    related_entity = table.Column<Guid>(type: "uuid", nullable: false),
                    is_opened = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discussions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "discussions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false),
                    discussion = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_discussions_discussion",
                        column: x => x.discussion,
                        principalSchema: "discussions",
                        principalTable: "discussions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_messages_discussion",
                schema: "discussions",
                table: "messages",
                column: "discussion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages",
                schema: "discussions");

            migrationBuilder.DropTable(
                name: "discussions",
                schema: "discussions");
        }
    }
}
