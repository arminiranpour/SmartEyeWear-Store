using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CART",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    CARTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    GUESTID = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    CREATEDAT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARTS", x => x.CARTID);
                });

            migrationBuilder.CreateTable(
                name: "CART_ITEM",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    CARTITEMID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CARTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    VARIANTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    QTY = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARTITEMS", x => x.CARTITEMID);
                    table.ForeignKey(
                        name: "FK_CARTITEM_CART",
                        column: x => x.CARTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "CART",
                        principalColumn: "CARTID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CARTITEM_VARIANT",
                        column: x => x.VARIANTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT_VARIANT",
                        principalColumn: "VARIANTID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARTS_GUESTID",
                schema: "DBS311_252NAA12",
                table: "CART",
                column: "GUESTID");

            migrationBuilder.CreateIndex(
                name: "IX_CARTS_USERID",
                schema: "DBS311_252NAA12",
                table: "CART",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_CART_ITEM_UNQ",
                schema: "DBS311_252NAA12",
                table: "CART_ITEM",
                columns: new[] { "CARTID", "VARIANTID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CARTITEMS_VARIANTID",
                schema: "DBS311_252NAA12",
                table: "CART_ITEM",
                column: "VARIANTID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CART_ITEM",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "CART",
                schema: "DBS311_252NAA12");
        }
    }
}
