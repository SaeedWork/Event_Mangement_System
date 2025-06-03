using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Mangement_System_WebTech_Project.Migrations
{
    /// <inheritdoc />
    public partial class FirstProperCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllDay",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Events",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Events",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Events",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "eventId");

            migrationBuilder.AddColumn<DateTime>(
                name: "endDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "eventTitle",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "eventTypeId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "locationId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "organizerId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    typeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    locationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    locationName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.locationId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    userRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_userRoleId",
                        column: x => x.userRoleId,
                        principalTable: "Roles",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Registerations",
                columns: table => new
                {
                    registerationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    attendeeId = table.Column<int>(type: "int", nullable: false),
                    eventId = table.Column<int>(type: "int", nullable: false),
                    registeredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registerations", x => x.registerationId);
                    table.ForeignKey(
                        name: "FK_Registerations_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "eventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registerations_Users_attendeeId",
                        column: x => x.attendeeId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_eventTypeId",
                table: "Events",
                column: "eventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_locationId_startDate_endDate",
                table: "Events",
                columns: new[] { "locationId", "startDate", "endDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_organizerId",
                table: "Events",
                column: "organizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Registerations_attendeeId_eventId",
                table: "Registerations",
                columns: new[] { "attendeeId", "eventId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registerations_eventId",
                table: "Registerations",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_userRoleId",
                table: "Users",
                column: "userRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_eventTypeId",
                table: "Events",
                column: "eventTypeId",
                principalTable: "EventTypes",
                principalColumn: "typeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_locationId",
                table: "Events",
                column: "locationId",
                principalTable: "Locations",
                principalColumn: "locationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_organizerId",
                table: "Events",
                column: "organizerId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTypes_eventTypeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_locationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_organizerId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Registerations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Events_eventTypeId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_locationId_startDate_endDate",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_organizerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "endDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "eventTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "eventTypeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "locationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "organizerId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Events",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Events",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Events",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "eventId",
                table: "Events",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "AllDay",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Events",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
