using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class NullableGuestID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GUESTID",
                schema: "DBS311_252NAA12",
                table: "USERINTERACTIONS",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<int>(
                name: "PRESCRIPTION",
                schema: "DBS311_252NAA12",
                table: "SURVEYANSWER",
                type: "NUMBER(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");

            migrationBuilder.AlterColumn<int>(
                name: "HASANTISCRATCHCOATING",
                schema: "DBS311_252NAA12",
                table: "GLASSESINFO",
                type: "NUMBER(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");

            migrationBuilder.AlterColumn<int>(
                name: "INSTOCK",
                schema: "DBS311_252NAA12",
                table: "GLASSES",
                type: "NUMBER(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GUESTID",
                schema: "DBS311_252NAA12",
                table: "USERINTERACTIONS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "PRESCRIPTION",
                schema: "DBS311_252NAA12",
                table: "SURVEYANSWER",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HASANTISCRATCHCOATING",
                schema: "DBS311_252NAA12",
                table: "GLASSESINFO",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "INSTOCK",
                schema: "DBS311_252NAA12",
                table: "GLASSES",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(1)",
                oldNullable: true);
        }
    }
}
