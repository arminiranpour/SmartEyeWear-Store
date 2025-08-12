using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class zero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DBS311_252NAA12");

            migrationBuilder.CreateTable(
                name: "BRAND",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    BRANDID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANDS", x => x.BRANDID);
                });

            migrationBuilder.CreateTable(
                name: "COLOR",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    COLORID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLORS", x => x.COLORID);
                });

            migrationBuilder.CreateTable(
                name: "FEATURE",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    FEATUREID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CODE = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    LABEL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FEATURES", x => x.FEATUREID);
                });

            migrationBuilder.CreateTable(
                name: "MATERIAL",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    MATERIALID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MATERIALS", x => x.MATERIALID);
                });

            migrationBuilder.CreateTable(
                name: "RIM_STYLE",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    RIMSTYLEID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIMSTYLES", x => x.RIMSTYLEID);
                });

            migrationBuilder.CreateTable(
                name: "SHAPE",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    SHAPEID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHAPES", x => x.SHAPEID);
                });

            migrationBuilder.CreateTable(
                name: "TAG",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    TAGID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAGS", x => x.TAGID);
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
                name: "PRODUCT",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SLUG = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BRANDID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SOURCEURL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ISACTIVE = table.Column<int>(type: "NUMBER(1)", nullable: false, defaultValue: 1),
                    CREATEDAT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSTIMESTAMP"),
                    UPDATEDAT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.PRODUCTID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_BRAND",
                        column: x => x.BRANDID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "BRAND",
                        principalColumn: "BRANDID");
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
                    FAVORITE_SHAPES = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    COLORS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MATERIALS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LENS_WIDTH = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    BRIDGE_WIDTH = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TEMPLE_LENGTH = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    HEAD_SIZE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SCREEN_TIME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DAY_LOCATION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRESCRIPTION = table.Column<int>(type: "NUMBER(1)", nullable: true),
                    FEATURES = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
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
                name: "FRAME_SPECS",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MATERIALID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    SHAPEID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    RIMSTYLEID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    WEIGHTG = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true),
                    GENDER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    NOTES = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FRAMESPECS", x => x.PRODUCTID);
                    table.ForeignKey(
                        name: "FK_FRAMESPECS_MATERIALS_MATERIALID",
                        column: x => x.MATERIALID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "MATERIAL",
                        principalColumn: "MATERIALID");
                    table.ForeignKey(
                        name: "FK_FRAMESPECS_RIMSTYLES_RIMSTYLEID",
                        column: x => x.RIMSTYLEID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "RIM_STYLE",
                        principalColumn: "RIMSTYLEID");
                    table.ForeignKey(
                        name: "FK_FRAMESPECS_SHAPES_SHAPEID",
                        column: x => x.SHAPEID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "SHAPE",
                        principalColumn: "SHAPEID");
                    table.ForeignKey(
                        name: "FK_FRAME_SPECS_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_FEATURE",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FEATUREID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTFEATURES", x => new { x.PRODUCTID, x.FEATUREID });
                    table.ForeignKey(
                        name: "FK_PRODUCT_FEATURE_FEATURE",
                        column: x => x.FEATUREID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "FEATURE",
                        principalColumn: "FEATUREID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_FEATURE_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_TAG",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TAGID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTTAGS", x => new { x.PRODUCTID, x.TAGID });
                    table.ForeignKey(
                        name: "FK_PRODUCT_TAG_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_TAG_TAG",
                        column: x => x.TAGID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "TAG",
                        principalColumn: "TAGID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_VARIANT",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    VARIANTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PRODUCTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COLORID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    SIZELABEL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SKU = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ISDEFAULT = table.Column<int>(type: "NUMBER(1)", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTVARIANTS", x => x.VARIANTID);
                    table.ForeignKey(
                        name: "FK_PRODUCTVARIANTS_COLORS_COLORID",
                        column: x => x.COLORID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "COLOR",
                        principalColumn: "COLORID");
                    table.ForeignKey(
                        name: "FK_PRODUCT_VARIANT_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RATING_SUMMARY",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AVGRATING = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true),
                    RATINGCOUNT = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RATINGSUMMARIES", x => x.PRODUCTID);
                    table.ForeignKey(
                        name: "FK_RATING_SUMMARY_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
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
                    VARIANTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    INTERACTIONTYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SCORE = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TIMESTAMP = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_INTERACTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UI_USR",
                        column: x => x.USERID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "USERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UI_VARIANT",
                        column: x => x.VARIANTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT_VARIANT",
                        principalColumn: "VARIANTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VARIANT_DIMENSIONS",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    VARIANTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LENSWIDTHMM = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    BRIDGEWIDTHMM = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    TEMPLELENGTHMM = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    LENSHEIGHTMM = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true),
                    FRAMEWIDTHMM = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VARIANTDIMENSIONS", x => x.VARIANTID);
                    table.ForeignKey(
                        name: "FK_VARIANT_DIMENSIONS",
                        column: x => x.VARIANTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT_VARIANT",
                        principalColumn: "VARIANTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VARIANT_IMAGE",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    IMAGEID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    VARIANTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    URL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ROLE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SORTORDER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VARIANTIMAGES", x => x.IMAGEID);
                    table.ForeignKey(
                        name: "FK_VARIANT_IMAGE_VARIANT",
                        column: x => x.VARIANTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT_VARIANT",
                        principalColumn: "VARIANTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VARIANT_INVENTORY",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    VARIANTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    QTYONHAND = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BACKORDERABLE = table.Column<int>(type: "NUMBER(1)", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VARIANTINVENTORIES", x => x.VARIANTID);
                    table.ForeignKey(
                        name: "FK_VARIANT_INVENTORY_VARIANT",
                        column: x => x.VARIANTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT_VARIANT",
                        principalColumn: "VARIANTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VARIANT_PRICE",
                schema: "DBS311_252NAA12",
                columns: table => new
                {
                    PRICEID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    VARIANTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CURRENCY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BASEPRICECENTS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SALEPRICECENTS = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    VALIDFROM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    VALIDTO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VARIANTPRICES", x => x.PRICEID);
                    table.ForeignKey(
                        name: "FK_VARIANT_PRICE_VARIANT",
                        column: x => x.VARIANTID,
                        principalSchema: "DBS311_252NAA12",
                        principalTable: "PRODUCT_VARIANT",
                        principalColumn: "VARIANTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BRANDS_NAME",
                schema: "DBS311_252NAA12",
                table: "BRAND",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COLORS_NAME",
                schema: "DBS311_252NAA12",
                table: "COLOR",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FEATURES_CODE",
                schema: "DBS311_252NAA12",
                table: "FEATURE",
                column: "CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FRAMESPECS_MATERIALID",
                schema: "DBS311_252NAA12",
                table: "FRAME_SPECS",
                column: "MATERIALID");

            migrationBuilder.CreateIndex(
                name: "IX_FRAMESPECS_RIMSTYLEID",
                schema: "DBS311_252NAA12",
                table: "FRAME_SPECS",
                column: "RIMSTYLEID");

            migrationBuilder.CreateIndex(
                name: "IX_FRAMESPECS_SHAPEID",
                schema: "DBS311_252NAA12",
                table: "FRAME_SPECS",
                column: "SHAPEID");

            migrationBuilder.CreateIndex(
                name: "IX_MATERIALS_NAME",
                schema: "DBS311_252NAA12",
                table: "MATERIAL",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_BRAND_ID",
                schema: "DBS311_252NAA12",
                table: "PRODUCT",
                column: "BRANDID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_SLUG",
                schema: "DBS311_252NAA12",
                table: "PRODUCT",
                column: "SLUG",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTFEATURES_FEATUREID",
                schema: "DBS311_252NAA12",
                table: "PRODUCT_FEATURE",
                column: "FEATUREID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTTAGS_TAGID",
                schema: "DBS311_252NAA12",
                table: "PRODUCT_TAG",
                column: "TAGID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTVARIANTS_COLORID",
                schema: "DBS311_252NAA12",
                table: "PRODUCT_VARIANT",
                column: "COLORID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTVARIANTS_PRODUCTID",
                schema: "DBS311_252NAA12",
                table: "PRODUCT_VARIANT",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "IX_RIMSTYLES_NAME",
                schema: "DBS311_252NAA12",
                table: "RIM_STYLE",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SHAPES_NAME",
                schema: "DBS311_252NAA12",
                table: "SHAPE",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SURVEY_ANSWERS_USER_ID",
                schema: "DBS311_252NAA12",
                table: "SURVEYANSWER",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TAGS_NAME",
                schema: "DBS311_252NAA12",
                table: "TAG",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_INTERACTIONS_USERID",
                schema: "DBS311_252NAA12",
                table: "USERINTERACTIONS",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_INTERACTIONS_VARIANTID",
                schema: "DBS311_252NAA12",
                table: "USERINTERACTIONS",
                column: "VARIANTID");

            migrationBuilder.CreateIndex(
                name: "IX_VARIANTIMAGES_VARIANTID",
                schema: "DBS311_252NAA12",
                table: "VARIANT_IMAGE",
                column: "VARIANTID");

            migrationBuilder.CreateIndex(
                name: "IX_VARIANTIMAGES_VARIANTID_SORTORDER",
                schema: "DBS311_252NAA12",
                table: "VARIANT_IMAGE",
                columns: new[] { "VARIANTID", "SORTORDER" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VARIANTPRICES_VARIANTID",
                schema: "DBS311_252NAA12",
                table: "VARIANT_PRICE",
                column: "VARIANTID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FRAME_SPECS",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "PRODUCT_FEATURE",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "PRODUCT_TAG",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "RATING_SUMMARY",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "SURVEYANSWER",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "USERINTERACTIONS",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "VARIANT_DIMENSIONS",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "VARIANT_IMAGE",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "VARIANT_INVENTORY",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "VARIANT_PRICE",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "MATERIAL",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "RIM_STYLE",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "SHAPE",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "FEATURE",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "TAG",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "PRODUCT_VARIANT",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "COLOR",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "PRODUCT",
                schema: "DBS311_252NAA12");

            migrationBuilder.DropTable(
                name: "BRAND",
                schema: "DBS311_252NAA12");
        }
    }
}
