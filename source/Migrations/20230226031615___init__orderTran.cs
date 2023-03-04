using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class initorderTran : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderTransports",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adultCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    childrenCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    Transportid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTransports", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderTransports_Transports_Transportid",
                        column: x => x.Transportid,
                        principalTable: "Transports",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTransports_Transportid",
                table: "OrderTransports",
                column: "Transportid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTransports");
        }
    }
}
