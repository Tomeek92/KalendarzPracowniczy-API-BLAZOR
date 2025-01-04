using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalendarzPracowniczyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CarInspection",
                table: "Cars",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarKm",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DaysOff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateDayOff = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayOffUser",
                columns: table => new
                {
                    DaysOffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOffUser", x => new { x.DaysOffId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_DayOffUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayOffUser_DaysOff_DaysOffId",
                        column: x => x.DaysOffId,
                        principalTable: "DaysOff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayOffUser_UsersId",
                table: "DayOffUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOffUser");

            migrationBuilder.DropTable(
                name: "DaysOff");

            migrationBuilder.DropColumn(
                name: "CarInspection",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarKm",
                table: "Cars");
        }
    }
}
