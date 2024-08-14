using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalVolunteer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    expirience_years = table.Column<int>(type: "integer", nullable: false),
                    pets_founded_home = table.Column<int>(type: "integer", nullable: false),
                    pets_looking_for_home = table.Column<int>(type: "integer", nullable: false),
                    pets_in_vet_clinic = table.Column<int>(type: "integer", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.volunteer_id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    pet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    species = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    breed = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    health_info = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_neutered = table.Column<bool>(type: "boolean", nullable: false),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    current_status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pet_id1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.pet_id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_pet_id",
                        column: x => x.pet_id1,
                        principalTable: "volunteers",
                        principalColumn: "volunteer_id");
                });

            migrationBuilder.CreateTable(
                name: "social_network",
                columns: table => new
                {
                    social_network_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    social_network_id1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_network", x => x.social_network_id);
                    table.ForeignKey(
                        name: "fk_social_network_volunteers_social_network_id",
                        column: x => x.social_network_id1,
                        principalTable: "volunteers",
                        principalColumn: "volunteer_id");
                });

            migrationBuilder.CreateTable(
                name: "payment_details",
                columns: table => new
                {
                    payment_detalis_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    payment_details_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_details", x => x.payment_detalis_id);
                    table.ForeignKey(
                        name: "fk_payment_details_pets_payment_details_id",
                        column: x => x.payment_details_id,
                        principalTable: "pets",
                        principalColumn: "pet_id");
                    table.ForeignKey(
                        name: "fk_payment_details_volunteers_payment_details_id",
                        column: x => x.payment_details_id,
                        principalTable: "volunteers",
                        principalColumn: "volunteer_id");
                });

            migrationBuilder.CreateTable(
                name: "pet_photos",
                columns: table => new
                {
                    pet_photo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_path = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    pet_photo_id1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_photos", x => x.pet_photo_id);
                    table.ForeignKey(
                        name: "fk_pet_photos_pets_pet_photo_id",
                        column: x => x.pet_photo_id1,
                        principalTable: "pets",
                        principalColumn: "pet_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_payment_details_payment_details_id",
                table: "payment_details",
                column: "payment_details_id");

            migrationBuilder.CreateIndex(
                name: "ix_pet_photos_pet_photo_id",
                table: "pet_photos",
                column: "pet_photo_id1");

            migrationBuilder.CreateIndex(
                name: "ix_pets_pet_id",
                table: "pets",
                column: "pet_id1");

            migrationBuilder.CreateIndex(
                name: "ix_social_network_social_network_id",
                table: "social_network",
                column: "social_network_id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_details");

            migrationBuilder.DropTable(
                name: "pet_photos");

            migrationBuilder.DropTable(
                name: "social_network");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
