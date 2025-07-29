using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class fromScratch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DBS311_252NAA12");

            migrationBuilder.CreateTable(
                name: "GLASSESINFO",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BRAND = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GENDER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SHAPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RIM = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    STYLE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HEADSIZE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SIZE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LENSWIDTH = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: true),
                    BRIDGEWIDTH = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: true),
                    TEMPLELENGTH = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: true),
                    WEIGHTGRAMS = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: true),
                    MATERIAL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FIT = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HASANTISCRATCHCOATING = table.Column<int>(type: "NUMBER(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLASSES_INFO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GLASSES",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GLASSESINFOID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COLOR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRICE = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    IMAGEURL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    INSTOCK = table.Column<int>(type: "NUMBER(1)", nullable: true),
                    POPULARITYSCORE = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    ISACTIVE = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLASSES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GLS_INFO",
                        column: x => x.GLASSESINFOID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "GLASSESINFO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GLASSES_FEATURES",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GLASSESINFOID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FEATURE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLASSES_FEATURES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GF_INFO",
                        column: x => x.GLASSESINFOID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "GLASSESINFO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SURVEYANSWER",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GENDER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    STYLE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LIFESTYLE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BUYING_FREQUENCY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRICE_FOCUS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FACE_SHAPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LENS_WIDTH = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    BRIDGE_WIDTH = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TEMPLE_LENGTH = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    HEAD_SIZE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SCREEN_TIME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DAY_LOCATION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRESCRIPTION = table.Column<int>(type: "NUMBER(1)", nullable: true),
                    USER_ID = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_ANSWERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SA_USER",
                        column: x => x.USER_ID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERINTERACTIONS",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    GUESTID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    GLASSID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    INTERACTIONTYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SCORE = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TIMESTAMP = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_INTERACTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UI_GLS",
                        column: x => x.GLASSID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "GLASSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UI_USR",
                        column: x => x.USERID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SURVEY_MULTI_CHOICES",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SURVEY_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    VALUE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_MULTI_CHOICES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SM_SURVEY",
                        column: x => x.SURVEY_ID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "SURVEYANSWER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GLA_GLA_1C8",
                schema: "DBS311_252NAA12",
                table: "GLASSES",
                column: "GLASSESINFOID");

            migrationBuilder.CreateIndex(
                name: "IX_GLA_GLA_FEA",
                schema: "DBS311_252NAA12",
                table: "GLASSES_FEATURES",
                column: "GLASSESINFOID");

            migrationBuilder.CreateIndex(
                name: "IX_SUR_SUR_A57",
                schema: "DBS311_252NAA12",
                table: "SURVEY_MULTI_CHOICES",
                column: "SURVEY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SUR_USE_C81",
                schema: "DBS311_252NAA12",
                table: "SURVEYANSWER",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USE_GLA_62B",
                schema: "DBS311_252NAA12",
                table: "USERINTERACTIONS",
                column: "GLASSID");

            migrationBuilder.CreateIndex(
                name: "IX_USE_USE_62B",
                schema: "DBS311_252NAA12",
                table: "USERINTERACTIONS",
                column: "USERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GLASSES_FEATURES",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "SURVEY_MULTI_CHOICES",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "USERINTERACTIONS",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "SURVEYANSWER",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "GLASSES",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "GLASSESINFO",
                schema: "DBS311_252NAA12");
        }
    }
}
