using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldToSalons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalonStatus",
                table: "Salons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalonStatusId",
                table: "Salons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salons_SalonStatusId",
                table: "Salons",
                column: "SalonStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salons_SalonStatuses_SalonStatusId",
                table: "Salons",
                column: "SalonStatusId",
                principalTable: "SalonStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salons_SalonStatuses_SalonStatusId",
                table: "Salons");

            migrationBuilder.DropIndex(
                name: "IX_Salons_SalonStatusId",
                table: "Salons");

            migrationBuilder.DropColumn(
                name: "SalonStatus",
                table: "Salons");

            migrationBuilder.DropColumn(
                name: "SalonStatusId",
                table: "Salons");
        }
    }
}
