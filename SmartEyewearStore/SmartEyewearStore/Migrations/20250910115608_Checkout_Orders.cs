using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class Checkout_Orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CLOSEDAT",
                schema: "DBS311_252NAA12",
                table: "CART",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ORDERS",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ORDERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ORDERNUMBER = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    GUESTID = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    CARTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CREATEDAT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FULLNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BILLINGADDRESS1 = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BILLINGADDRESS2 = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    BILLINGCITY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BILLINGSTATE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BILLINGPOSTALCODE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BILLINGCOUNTRY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SHIPTODIFFERENT = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    SHIPPINGFULLNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SHIPPINGPHONE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SHIPPINGADDRESS1 = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SHIPPINGADDRESS2 = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SHIPPINGCITY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SHIPPINGSTATE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SHIPPINGPOSTALCODE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SHIPPINGCOUNTRY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SUBTOTALCENTS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SHIPPINGCENTS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TAXCENTS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TOTALCENTS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.ORDERID);
                    table.ForeignKey(
                        name: "FK_ORDER_CART",
                        column: x => x.CARTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "CART",
                        principalColumn: "CARTID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDERITEM",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ORDERITEMID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ORDERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    VARIANTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PRODUCTNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    VARIANTLABEL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UNITPRICECENTS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    QTY = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERITEMS", x => x.ORDERITEMID);
                    table.ForeignKey(
                        name: "FK_ORDERITEM_ORDER",
                        column: x => x.ORDERID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "ORDERS",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARTS_CLOSEDAT",
                schema: "DBS311_252NAA12",
                table: "CART",
                column: "CLOSEDAT");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERITEMS_ORDERID",
                schema: "DBS311_252NAA12",
                table: "ORDERITEM",
                column: "ORDERID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_CARTID",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                column: "CARTID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_GUESTID",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                column: "GUESTID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_ORDERNUMBER",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                column: "ORDERNUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_USERID",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                column: "USERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDERITEM",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "ORDERS",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropIndex(
                name: "IX_CARTS_CLOSEDAT",
                schema: "DBS311_252NAA12",
                table: "CART");

            migrationBuilder.DropColumn(
                name: "CLOSEDAT",
                schema: "DBS311_252NAA12",
                table: "CART");
        }
    }
}
