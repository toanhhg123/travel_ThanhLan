using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class updateupdatehotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_CategoryTours_categoryTourid",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_categoryTourid",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "categoryTourid",
                table: "Hotels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "categoryTourid",
                table: "Hotels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_categoryTourid",
                table: "Hotels",
                column: "categoryTourid");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_CategoryTours_categoryTourid",
                table: "Hotels",
                column: "categoryTourid",
                principalTable: "CategoryTours",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
