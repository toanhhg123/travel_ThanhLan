using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class updateupdateimgHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Tours_Tourid",
                table: "HotelImages");

            migrationBuilder.DropIndex(
                name: "IX_HotelImages_Tourid",
                table: "HotelImages");

            migrationBuilder.DropColumn(
                name: "Tourid",
                table: "HotelImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tourid",
                table: "HotelImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_Tourid",
                table: "HotelImages",
                column: "Tourid");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Tours_Tourid",
                table: "HotelImages",
                column: "Tourid",
                principalTable: "Tours",
                principalColumn: "id");
        }
    }
}
