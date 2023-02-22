using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class fixorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_OrderTours_OrderTourid",
                table: "HotelImages");

            migrationBuilder.DropIndex(
                name: "IX_HotelImages_OrderTourid",
                table: "HotelImages");

            migrationBuilder.DropColumn(
                name: "OrderTourid",
                table: "HotelImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "OrderTours",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "OrderTours");

            migrationBuilder.AddColumn<string>(
                name: "OrderTourid",
                table: "HotelImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_OrderTourid",
                table: "HotelImages",
                column: "OrderTourid");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_OrderTours_OrderTourid",
                table: "HotelImages",
                column: "OrderTourid",
                principalTable: "OrderTours",
                principalColumn: "id");
        }
    }
}
