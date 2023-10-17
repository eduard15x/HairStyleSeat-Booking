using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkHoursFieldsToSalonsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkHoursInterval",
                table: "Salons",
                newName: "StartTimeHour");

            migrationBuilder.AddColumn<string>(
                name: "EndTimeHour",
                table: "Salons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HaircutDurationTime",
                table: "Salons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTimeHour",
                table: "Salons");

            migrationBuilder.DropColumn(
                name: "HaircutDurationTime",
                table: "Salons");

            migrationBuilder.RenameColumn(
                name: "StartTimeHour",
                table: "Salons",
                newName: "WorkHoursInterval");
        }
    }
}
