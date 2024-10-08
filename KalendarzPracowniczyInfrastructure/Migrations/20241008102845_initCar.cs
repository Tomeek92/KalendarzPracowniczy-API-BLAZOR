using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalendarzPracowniczyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Car",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "Car",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
