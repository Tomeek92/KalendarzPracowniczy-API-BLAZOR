using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalendarzPracowniczyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCarBusy",
                table: "Cars",
                newName: "Production");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Production",
                table: "Cars",
                newName: "DateCarBusy");
        }
    }
}
