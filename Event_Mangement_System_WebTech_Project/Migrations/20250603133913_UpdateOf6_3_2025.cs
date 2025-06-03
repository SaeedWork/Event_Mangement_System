using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Mangement_System_WebTech_Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOf6_3_2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "Registerations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "Registerations");
        }
    }
}
