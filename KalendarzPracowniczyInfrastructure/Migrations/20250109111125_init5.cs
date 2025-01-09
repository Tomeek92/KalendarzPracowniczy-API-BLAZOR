using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalendarzPracowniczyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarEvent");

            migrationBuilder.DropColumn(
                name: "BusyDate",
                table: "Cars");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Events",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CarId",
                table: "Events",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Cars_CarId",
                table: "Events",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Cars_CarId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CarId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Events");

            migrationBuilder.AddColumn<DateTime>(
                name: "BusyDate",
                table: "Cars",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarEvent",
                columns: table => new
                {
                    CarsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEvent", x => new { x.CarsId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_CarEvent_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarEvent_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CarEvent_EventsId",
                table: "CarEvent",
                column: "EventsId");
        }
    }
}
