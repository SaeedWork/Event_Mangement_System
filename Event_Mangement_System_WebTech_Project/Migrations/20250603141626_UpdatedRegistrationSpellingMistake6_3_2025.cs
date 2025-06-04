using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Mangement_System_WebTech_Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRegistrationSpellingMistake6_3_2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "registerationId",
                table: "Registrations",
                newName: "registrationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "registrationId",
                table: "Registrations",
                newName: "registerationId");
        }
    }
}
