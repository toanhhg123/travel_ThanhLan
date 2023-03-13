using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class updatevisa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visas",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mainImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    info = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visas", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visas");
        }
    }
}
