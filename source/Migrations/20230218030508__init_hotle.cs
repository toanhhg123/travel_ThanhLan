using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class inithotle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    openTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mainImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    desc = table.Column<string>(type: "ntext", nullable: true),
                    info = table.Column<string>(type: "ntext", nullable: true),
                    schedule = table.Column<string>(type: "ntext", nullable: true),
                    departureSchedule = table.Column<string>(type: "ntext", nullable: true),
                    categoryTourid = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hotels_CategoryTours_categoryTourid",
                        column: x => x.categoryTourid,
                        principalTable: "CategoryTours",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    src = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tourid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Hotelid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.id);
                    table.ForeignKey(
                        name: "FK_HotelImages_Hotels_Hotelid",
                        column: x => x.Hotelid,
                        principalTable: "Hotels",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_HotelImages_Tours_Tourid",
                        column: x => x.Tourid,
                        principalTable: "Tours",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_Hotelid",
                table: "HotelImages",
                column: "Hotelid");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_Tourid",
                table: "HotelImages",
                column: "Tourid");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_categoryTourid",
                table: "Hotels",
                column: "categoryTourid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
