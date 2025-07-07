using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class BuildFromScratch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GLASSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FRAMESHAPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    COLOR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    STYLE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    USAGE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRICE = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    IMAGEURL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLASSES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
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
                name: "SURVEY_ANSWERS",
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
                    PRESCRIPTION = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    FEATURES = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    USER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_ANSWERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SA_USER",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SURVEY_ANSWERS_USER_ID",
                table: "SURVEY_ANSWERS",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GLASSES");

            migrationBuilder.DropTable(
                name: "SURVEY_ANSWERS");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
