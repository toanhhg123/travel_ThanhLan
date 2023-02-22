using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class addordertour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderTourid",
                table: "HotelImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderTours",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adultCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    childrenCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tourid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTours", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderTours_Tours_Tourid",
                        column: x => x.Tourid,
                        principalTable: "Tours",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_OrderTourid",
                table: "HotelImages",
                column: "OrderTourid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTours_Tourid",
                table: "OrderTours",
                column: "Tourid");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_OrderTours_OrderTourid",
                table: "HotelImages",
                column: "OrderTourid",
                principalTable: "OrderTours",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_OrderTours_OrderTourid",
                table: "HotelImages");

            migrationBuilder.DropTable(
                name: "OrderTours");

            migrationBuilder.DropIndex(
                name: "IX_HotelImages_OrderTourid",
                table: "HotelImages");

            migrationBuilder.DropColumn(
                name: "OrderTourid",
                table: "HotelImages");
        }
    }
}
