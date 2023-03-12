using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace source.Migrations
{
    /// <inheritdoc />
    public partial class initserver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTours",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTours", x => x.id);
                });

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
                    departureSchedule = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

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
                name: "Tours",
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
                    table.PrimaryKey("PK_Tours", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tours_CategoryTours_categoryTourid",
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
                });

            migrationBuilder.CreateTable(
                name: "OrderHotels",
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
                    Hotelid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHotels", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderHotels_Hotels_Hotelid",
                        column: x => x.Hotelid,
                        principalTable: "Hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    userName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hashPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    Roleid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_Roleid",
                        column: x => x.Roleid,
                        principalTable: "Roles",
                        principalColumn: "id");
                });

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
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TourImages",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    src = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tourid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourImages", x => x.id);
                    table.ForeignKey(
                        name: "FK_TourImages_Tours_Tourid",
                        column: x => x.Tourid,
                        principalTable: "Tours",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Roleid",
                table: "Accounts",
                column: "Roleid");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_Hotelid",
                table: "HotelImages",
                column: "Hotelid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHotels_Hotelid",
                table: "OrderHotels",
                column: "Hotelid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTours_Tourid",
                table: "OrderTours",
                column: "Tourid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTransports_Transportid",
                table: "OrderTransports",
                column: "Transportid");

            migrationBuilder.CreateIndex(
                name: "IX_TourImages_Tourid",
                table: "TourImages",
                column: "Tourid");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_categoryTourid",
                table: "Tours",
                column: "categoryTourid");

            migrationBuilder.CreateIndex(
                name: "IX_TransportImages_Transportid",
                table: "TransportImages",
                column: "Transportid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "OrderHotels");

            migrationBuilder.DropTable(
                name: "OrderTours");

            migrationBuilder.DropTable(
                name: "OrderTransports");

            migrationBuilder.DropTable(
                name: "TourImages");

            migrationBuilder.DropTable(
                name: "TransportImages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "CategoryTours");
        }
    }
}
