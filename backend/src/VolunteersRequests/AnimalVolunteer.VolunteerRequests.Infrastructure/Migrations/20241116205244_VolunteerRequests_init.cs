using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VolunteerRequests_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "volunteer_requests");

            migrationBuilder.CreateTable(
                name: "volunteer_requests",
                schema: "volunteer_requests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    admin_id = table.Column<Guid>(type: "uuid", nullable: false),
                    discussion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status_enum = table.Column<string>(type: "text", nullable: false),
                    rejection_comment = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer_requests", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "volunteer_requests",
                schema: "volunteer_requests");
        }
    }
}
