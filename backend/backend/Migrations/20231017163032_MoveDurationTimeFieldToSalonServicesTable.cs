using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class MoveDurationTimeFieldToSalonServicesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaircutDurationTime",
                table: "Salons");

            migrationBuilder.AddColumn<string>(
                name: "HaircutDurationTime",
                table: "SalonServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaircutDurationTime",
                table: "SalonServices");

            migrationBuilder.AddColumn<string>(
                name: "HaircutDurationTime",
                table: "Salons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
