using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class inittranport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transports",
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
                    departureSchedule = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TransportImages",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    src = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transportid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportImages", x => x.id);
                    table.ForeignKey(
                        name: "FK_TransportImages_Transports_Transportid",
                        column: x => x.Transportid,
                        principalTable: "Transports",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportImages_Transportid",
                table: "TransportImages",
                column: "Transportid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportImages");

            migrationBuilder.DropTable(
                name: "Transports");
        }
    }
}
